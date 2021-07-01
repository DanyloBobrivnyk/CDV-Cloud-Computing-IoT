using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IoT_Project_Food_Ordering.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersView : ContentPage
    {
        public OrdersView(string id)
        {
            InitializeComponent();
            LabeOrderID.Text = id;
            //We may do some other operations with id
        }
        private async void ImageButton_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void Button_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ProductsView());
        }

        private async void Button_ClickedCopy(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(LabeOrderID.Text);
        }
    }
}