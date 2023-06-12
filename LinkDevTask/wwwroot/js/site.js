function ConfirmDelete() {
    return confirm("Are you sure?") ? true : event.preventDefault();
}