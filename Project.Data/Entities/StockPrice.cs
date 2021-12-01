using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Entities
{
    public class StockPrice : BaseEntity
    {
        [Required]
        public string Ticker { get; set; }

        public DateTime TradeDate { get; set; }

        public decimal Open { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Close { get; set; }

        public decimal Volume { get; set; }

        public decimal Change { get; set; }

        public decimal ChangePercent { get; set; }
    }
}
