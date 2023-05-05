using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Infraestructure.Dtos.Response;
using Infraestructure.Helpers;
using Infraestructure.Interfaces;
using Infraestructure.Setings;
using Microsoft.Extensions.Options;

namespace Infraestructure.Repositories
{
    public class ProductsRepository : SqlServerBase<Products>, IProductsRepository
    {
        private readonly string _spGetProducts;

        public ProductsRepository(
       IOptions<InfraestructureSettings> settings
        ) : base(settings.Value.SqlSettings.ConnectionString)
        {
            _spGetProducts = settings.Value.SqlSettings.SpGetProducts;
        }

        public async Task<List<GetProudctsResponseDto>> GetProuducts()
        {
            List<GetProudctsResponseDto> response = new();
            try
            {
                using (var rdr = await ExecuteSpResults(_spGetProducts))
                {
                    for (int i = 0; i < rdr.Tables[0].Rows.Count; i++)
                    {
                        var item = new GetProudctsResponseDto
                        {
                            ProudctId = rdr.Tables[0].Rows[i]["productid"].ToString(),
                            ProudctName = rdr.Tables[0].Rows[i]["productname"].ToString(),
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
