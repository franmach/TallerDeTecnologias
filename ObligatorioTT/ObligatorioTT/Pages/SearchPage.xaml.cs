using ObligatorioTT.ViewModels;

namespace ObligatorioTT.Pages
{
    public partial class SearchPage : ContentPage
    {
        private readonly SearchViewModel _viewModel;
        public SearchPage(SearchViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }
    }
}