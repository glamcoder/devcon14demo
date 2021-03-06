using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;

namespace devcon14demoDroidOffline
{
    public class NewsItemAdapter : BaseAdapter<NewsItem>
    {
        private Activity activity;
        private int layoutResourceId;
        private List<NewsItem> items = new List<NewsItem>();

        public NewsItemAdapter(Activity activity, int layoutResourceId)
        {
            this.activity = activity;
            this.layoutResourceId = layoutResourceId;
        }

        //Returns the view for a specific item on the list
        public override View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
        {
            var row = convertView;
            var currentItem = this[position];
            TextView checkBoxTitle;
            TextView textViewNewsText;

            if (row == null)
            {
                var inflater = activity.LayoutInflater;
                row = inflater.Inflate(layoutResourceId, parent, false);

                checkBoxTitle = row.FindViewById<TextView>(Resource.Id.newsItemTitle);
                textViewNewsText = row.FindViewById<TextView>(Resource.Id.newsItemText);
            }
            else
            {
                checkBoxTitle = row.FindViewById<TextView>(Resource.Id.newsItemTitle);
                textViewNewsText = row.FindViewById<TextView>(Resource.Id.newsItemText);
            }

            checkBoxTitle.Text = currentItem.Title;
            checkBoxTitle.Tag = new NewsWrapper(currentItem);

            textViewNewsText.Text = currentItem.Text;

            return row;
        }

        public void Add(NewsItem item)
        {
            items.Add(item);
            NotifyDataSetChanged();
        }

        public void Clear()
        {
            items.Clear();
            NotifyDataSetChanged();
        }

        public void Remove(NewsItem item)
        {
            items.Remove(item);
            NotifyDataSetChanged();
        }

        #region implemented abstract members of BaseAdapter

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override NewsItem this[int position]
        {
            get
            {
                return items[position];
            }
        }

        #endregion
    }
}

