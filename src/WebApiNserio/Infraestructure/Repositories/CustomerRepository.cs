using Domain.Models;
using Infraestructure.Dtos.Response;
using Infraestructure.Helpers;
using Infraestructure.Interfaces;
using Infraestructure.Setings;
using Microsoft.Extensions.Options;

namespace Infraestructure.Repositories
{
    public class CustomerRepository : SqlServerBase<Customer>, ICustomerRepository
    {
        private readonly string _spSalesPredictedDate;
        public CustomerRepository(
        IOptions<InfraestructureSettings> settings
         ) : base(settings.Value.SqlSettings.ConnectionString)
        {
            _spSalesPredictedDate = settings.Value.SqlSettings.SpSalesPredictedDate;
        }

        public async Task<List<GetCustomerPredictedDateDto>> GetCustomerPredictedDate()
        {
            List<GetCustomerPredictedDateDto> response = new();
            try
            {
                using (var rdr = await ExecuteSpResults(_spSalesPredictedDate))
                {
                    for (int i = 0; i < rdr.Tables[0].Rows.Count; i++)
                    {
                        var item = new GetCustomerPredictedDateDto
                        {
                            CustomerName = rdr.Tables[0].Rows[i]["custid"].ToString(),
                            LastOrderDate = Convert.ToDateTime(rdr.Tables[0].Rows[i]["lastorderdate"]),
                            NextPredictedOrder = Convert.ToDateTime(rdr.Tables[0].Rows[i]["nextpredictedorder"]),
                        };
                        response.Add(item);
                    }

                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
