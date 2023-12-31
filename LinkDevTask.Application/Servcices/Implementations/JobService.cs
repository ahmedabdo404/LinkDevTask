﻿using LinkDevTask.Application.Helpers;
using LinkDevTask.Domain.Models;
using LinkDevTask.Application.ViewModels.Job;
using LinkDevTask.Domain.DataAccess;
using LinkDevTask.Application.Servcices.Interfaces;
using Microsoft.AspNetCore.Http;
using LinkDevTask.Application.Consts;
using System.Dynamic;

namespace LinkDevTask.Application.Servcices.Implementations
{
    public class JobService : IJobService
    {
        #region ctor

        private readonly IUnitOfWork _unitOfWork;
        public JobService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        public async Task<PagedJobVM> GetJobsByPage(PageVM page, string searchValue)
        {
            (var Jobs, var count) = await _unitOfWork._jobRepo
                .GetByPage(page.start, page.length);

            IEnumerable<Job> data = Jobs.ToList();
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                data = data.Where(job => job.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase));
            }

            return new PagedJobVM()
            {
                RecordsFiltered = count,
                Data = data.Select(job => {
                    var jobVm = Mapper.MapTo<JobVM>(job);
                    jobVm.Id = job.Id;
                    return jobVm;
                })
            };
        }

        public int GetJobsCount()
        {
            return _unitOfWork._jobRepo.GetCount();
        }

        public IEnumerable<JobVM> GetAll()
        {
            var Jobs = _unitOfWork._jobRepo.GetAll(jc => jc.Category);
            foreach (var job in Jobs)
            {
                var jobVm = Mapper.MapTo<JobVM>(job);
                jobVm.Id = job.Id;
                jobVm.CategoryName = job.Category.Name;
                yield return jobVm;
            }
        }
        
        public JobVM? GetJob(string id)
        {
            var job = _unitOfWork._jobRepo.GetOne(job => job.Id.Equals(id), jc => jc.Category);
            if(job is not null)
            {
                var jobVm = Mapper.MapTo<JobVM>(job);
                jobVm.Id = job.Id;
                jobVm.CategoryName = job.Name;
                return jobVm;
            }

            return default;
        }

        public async Task<int> AddJob(JobVM newJob, IFormFile file)
        {
            var job = Mapper.MapTo<Job>(newJob);
            job.Picture = await HandleJobPicture(file);
            _unitOfWork._jobRepo.Add(job);
            return await _unitOfWork.SaveAsync();
        }

        private async Task<byte[]?> HandleJobPicture(IFormFile file)
        {
            if (file is not null)
            {
                using var DataStream = new MemoryStream();
                await file.CopyToAsync(DataStream);
                DataStream.Close();
                return DataStream.ToArray();
            }
            return Array.Empty<byte>();
        }

        public async Task<int> EditJob(JobVM newJob, IFormFile file)
        {
            var job = _unitOfWork._jobRepo.Find(newJob.Id!);
            if (job is not null)
            {
                Mapper.MapTo(job, newJob);
                job.Picture = await HandleJobPicture(file);
                _unitOfWork._jobRepo.Update(job);
            }
            return await _unitOfWork.SaveAsync();
        }

        public async Task<int> DeleteJob(string id)
        {
                
            var job = _unitOfWork._jobRepo.Find(id);
            if(job is not null)
            {
                _unitOfWork._jobRepo.Delete(job);
                return await _unitOfWork.SaveAsync();
            }
            return default;
        }
    }
}
