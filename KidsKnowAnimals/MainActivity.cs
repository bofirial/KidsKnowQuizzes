using System;
using System.Collections.Generic;
using Android.App;
using Android.Gms.Cast.Framework;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Microsoft.AspNetCore.SignalR.Client;

namespace KidsKnowQuizzes.KidsKnowAnimals
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : FragmentActivity
    {
        private HubConnection _connection;
        private ListView _listView;
        private Button _connectButton;
        private ArrayAdapter _messages;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            
            //Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            //SetSupportActionBar(toolbar);

            //FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            //fab.Click += FabOnClick;
            
            var imageButton1 = FindViewById<ImageButton>(Resource.Id.multipleChoice1);
            imageButton1.Click += MultipleChoiceOnClick;

            var imageButton2 = FindViewById<ImageButton>(Resource.Id.multipleChoice2);
            imageButton2.Click += MultipleChoiceOnClick;

            var imageButton3 = FindViewById<ImageButton>(Resource.Id.multipleChoice3);
            imageButton3.Click += MultipleChoiceOnClick;

            var imageButton4 = FindViewById<ImageButton>(Resource.Id.multipleChoice4);
            imageButton4.Click += MultipleChoiceOnClick;
            
            var mediaRouteButton =
                FindViewById<Android.Support.V7.App.MediaRouteButton>(Resource.Id.media_route_button);

            CastButtonFactory.SetUpMediaRouteButton(Application.Context, mediaRouteButton);

            //var castContext = CastContext.GetSharedInstance(this);

            _listView = FindViewById<ListView>(Resource.Id.messages);
            _connectButton = FindViewById<Button>(Resource.Id.bConnect);

            _messages = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, new List<string>());
            _listView.Adapter = _messages;

            _connectButton.Click += ConnectButton_Click;
        }

        private async void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                _connection = new HubConnectionBuilder()
                    .WithUrl("http://kidsknowquizzes.azurewebsites.net/hubs/quiz")
                    //.WithUrl("http://james-pc/hubs/quiz")
                    //.ConfigureLogging(logging => { logging.AddDebug(); })
                    .Build();

                _connection.On<string>("test", AppendMessage);

                await _connection.StartAsync();

                await _connection.SendAsync("RegisterTest", Guid.NewGuid());

            }
            catch (Exception ex)
            {
                AppendMessage($"An error occurred while connecting: {ex}");
            }
        }

        private void AppendMessage(string message)
        {
            RunOnUiThread(() =>
            {
                _messages.Add(message);
                _messages.NotifyDataSetChanged();

                _listView.SmoothScrollToPosition(_messages.Count - 1);
            });
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        //private void FabOnClick(object sender, EventArgs eventArgs)
        //{
        //    View view = (View) sender;
        //    Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
        //        .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        //}

        private void MultipleChoiceOnClick(object sender, EventArgs eventArgs)
        {
            var view = (View)sender;

            var animalName = view.TooltipText;

            Snackbar.Make(view, $"You selected the {animalName}!", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();

            var display = FindViewById<TextView>(Resource.Id.display);
            display.Text = animalName;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

