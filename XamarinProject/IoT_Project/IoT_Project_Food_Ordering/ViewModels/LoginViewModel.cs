using IoT_Project_Food_Ordering.Models;
using IoT_Project_Food_Ordering.Services;
using IoT_Project_Food_Ordering.Views;
using Lab1_bobrivnyk.Rest.DTOs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IoT_Project_Food_Ordering.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _Username;
        public string Username
        {
            set
            {
                this._Username = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Username;
            }
        }

        private string _Password;
        public string Password
        {
            set
            {
                this._Password = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Password;
            }
        }


        private bool _IsBusy;
        public bool IsBusy
        {
            set
            {
                this._IsBusy = value;
                OnPropertyChanged();
            }
            get
            {
                return this._IsBusy;
            }
        }

        private bool _Result;
        public bool Result
        {
            set
            {
                this._Result = value;
                OnPropertyChanged();
            }
            get
            {
                return this._Result;
            }
        }

        private bool _Disable;
        public bool Disable
        {
            set
            {
                _Disable = value;
                OnPropertyChanged();
            }

            get
            {
                return _Disable;
            }
        }

        public Command LoginCommand { get; set; }
        public Command RegisterCommand { get; set; }

        public LoginViewModel()
        {
            Disable = false;

            LoginCommand = new Command(async () => await LoginCommandAsync());
            RegisterCommand = new Command(async () => await RegisterCommandAsync());
        }

        private async Task RegisterCommandAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var userService = new UserService();
                //var userService = DependencyService.Get<IUserService>();
                Result = await userService.RegisterUser(Username, Password);
                if (Result)
                    await Application.Current.MainPage.DisplayAlert("Success", "User Registered", "OK");
                else
                    await Application.Current.MainPage.DisplayAlert("Error",
                        "User with such username already exists", "OK");

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoginCommandAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var userService = new UserService();

                //var userService = DependencyService.Get<IUserService>();
                
                string Result = await userService.LoginUser(Username, Password);
                if (Result.Length > 1)
                {
                    Preferences.Set("Username", Username);
                    /*var cn = DependencyService.Get<ISqLite>().GetConnection();

                    var item = cn.Table<JwtToken>().ToList();

                    if(item.Count < 0)
                    {
                        cn.Insert(Result);
                    }
                    else
                    {
                        var token = item.First();

                        token.JwtTokenValue = Result;
                        cn.Update(token);
                    }

                    cn.Commit();
                    cn.Close();*/

                    await Application.Current.MainPage.Navigation.PushModalAsync(new ProductsView());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Invalid Username or Password", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void UploadDataFromSqLite()
        {

        }
    }
}
