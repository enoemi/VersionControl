using gyakorlat_6.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gyakorlat_6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GetExchangeRatesRequestBody();
        }

        

        var mnbService = new MNBArfolyamServiceSoapClient();

        var request = new GetExchangeRatesRequestBody()
        {
            currencyNames = "EUR",
            startDate = "2020-01-01",
            endDate = "2020-06-30"
        };

        var response = MnbService.GetExchangeRates(request);

        var result = response.GetExchangeRatesResult;

        
    }
}
