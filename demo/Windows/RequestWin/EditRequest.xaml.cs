using demo.Data;
using demo.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;

namespace demo.Windows.RequestWin
{
    public partial class EditRequest : Window
    {
        private DemoContext context;
        private Order order;
        public EditRequest(Order order)
        {
            InitializeComponent();
            context = new DemoContext();
            PanelOrder.DataContext = order;
            this.order = order;
            BoxStatus.ItemsSource = context.Statuses.ToList();
            BoxStatus.SelectedItem = order.Status;
            var pickupPoints = context.PickupPoints.OrderBy(p => p.Adress).ToList();
            BoxPickupPoint.ItemsSource = pickupPoints;
            int? pickupId = order.PickupPointId ?? order.PickupPoint?.Id;
            if (pickupId.HasValue)
                BoxPickupPoint.SelectedItem = pickupPoints.FirstOrDefault(p => p.Id == pickupId.Value);
        }

        private void Button_save(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(BoxDateDelivery.Text) &&
                !string.IsNullOrWhiteSpace(BoxDateOrder.Text) &&
                !string.IsNullOrWhiteSpace(BoxArc.Text) &&
                BoxPickupPoint.SelectedItem is PickupPoint pickup)
            {
                try
                {

                    order.OrderDate = DateTime.Parse(BoxDateOrder.Text);
                    order.DeliveryDate = DateTime.Parse(BoxDateDelivery.Text);
                    order.Code = double.Parse(BoxArc.Text);
                    order.PickupPoint = pickup;
                    order.Status = BoxStatus.SelectedItem as Status;
                    
                    context.Entry(order).State = EntityState.Modified;
                    context.SaveChanges();

                    DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_exit(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
