using AndroidX.Lifecycle;
using Quick_Tasker.ViewModels;
using Quick_Tasker.Models;
namespace Quick_Tasker.Pages;

public partial class TaskBank : ContentPage
{
    private TaskViewModel viewModel;
    public TaskBank()
	{
        BindingContext = viewModel = new TaskViewModel();
        InitializeComponent();
	}
}
