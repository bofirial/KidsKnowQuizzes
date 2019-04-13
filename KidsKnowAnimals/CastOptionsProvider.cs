using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Cast.Framework;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace KidsKnowQuizzes.KidsKnowAnimals
{
    [Register("KidsKnowQuizzes/KidsKnowAnimals/CastOptionsProvider")]
    public class CastOptionsProvider : Java.Lang.Object, IOptionsProvider
    {
        public IList<SessionProvider> GetAdditionalSessionProviders(Context appContext)
        {
            return null;
        }

        public CastOptions GetCastOptions(Context appContext)
        {
            return new CastOptions.Builder()
                .SetReceiverApplicationId(appContext.GetString(Resource.String.app_id))
                .Build();
        }
    }
}