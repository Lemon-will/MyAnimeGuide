using System;
using System.Windows;
using MyAnimeGuide;
using MyAnimeGuide.ViewModel;

namespace MyAnimeGuide
{
    /// <summary>
    /// SelectChWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SelectChWindow : Window
    {
        public SelectChWindow()
        {
            this.InitializeComponent();
            SelectChWindowViewModel viewModel = new SelectChWindowViewModel();
            this.DataContext = viewModel;
            if (viewModel.CloseAction == null)
            {
                //() => hogehoge は入力なしのラムダ式(ここでは、ウィンドウをクローズすることをViewmodelのCloseActionにポインタとして渡す)
                viewModel.CloseAction = new Action(() => this.Close());
            }
        }
    }
}
