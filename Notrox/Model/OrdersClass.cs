using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Notrox.ViewModel;

namespace Notrox.Model
{
    public class OrdersClass : ViewModelBase
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Phase { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }

        private string _selectedPhase;
        public string SelectedPhase { get => _selectedPhase; set { _selectedPhase = value; OnPropertyChanged(); } }

        [JsonPropertyName("OrderItems")]
        public List<OrderItemClass>? Items { get; set; }

        public decimal TotalPrice => Items?.Sum(i => i.PriceAtPurchase * i.Quantity) ?? 0;

    }
}
