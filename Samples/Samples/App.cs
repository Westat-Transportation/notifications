﻿using System;
using System.Diagnostics;
using Acr.Notifications;
using Xamarin.Forms;


namespace Samples {

    public class App : Application {
        public static bool IsInBackgrounded { get; private set; }


        public App() {
            Notification.DefaultTitle = "Test Title";

            this.MainPage = new ContentPage {
                Content = new StackLayout {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        new Button {
                            Text = "Set Badge",
                            Command = new Command(() => Notifications.Instance.Badge = 1)
                        },
                        new Button {
                            Text = "Press This & Exit App within 10 seconds",
                            Command = new Command(() =>
                                Notifications.Instance.Send(new Notification()
                                    .SetMessage("Hello from the ACR Sample Notification App")
                                    .SetSchedule(TimeSpan.FromSeconds(10))
                                )
                            )
                        },
                        new Button
                        {
                            Text = "Multiple Timed Messages (5 seconds each x 10 messages)",
                            Command = new Command(() =>
                            {
                                for (var i = 1; i < 11; i++)
                                {
                                    var seconds = i * 5;
                                    var dateTime = DateTime.Now.AddSeconds(seconds);
                                    var id = Notifications.Instance.Send(new Notification()
                                        .SetMessage($"Message {i}")
                                        .SetSchedule(dateTime)
                                    );
                                    Debug.WriteLine($"Notification ID: {id}");
                                }
                            })
                        },
                        new Button {
                            Text = "Cancel All Notifications",
                            Command = new Command(Notifications.Instance.CancelAll)
                        }
                    }
                }
            };
        }


        protected override void OnResume() {
            base.OnResume();
            App.IsInBackgrounded = false;
        }


        protected override void OnSleep() {
            base.OnSleep();
            App.IsInBackgrounded = true;
        }
    }
}
