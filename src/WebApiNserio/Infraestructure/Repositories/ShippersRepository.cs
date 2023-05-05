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
    public class ShippersRepository : SqlServerBase<Shippers>, IShippersRepository
    {
        private readonly string _spGetShippers;

        public ShippersRepository(
       IOptions<InfraestructureSettings> settings
        ) : base(settings.Value.SqlSettings.ConnectionString)
        {
            _spGetShippers = settings.Value.SqlSettings.SpGetShippers;
        }
        public async Task<List<GetShippersResponseDto>> GetShippers()
        {
            List<GetShippersResponseDto> response = new();
            try
            {
                using (var rdr = await ExecuteSpResults(_spGetShippers))
                {
                    for (int i = 0; i < rdr.Tables[0].Rows.Count; i++)
                    {
                        var item = new GetShippersResponseDto
                        {
                            ShipperId = rdr.Tables[0].Rows[i]["shipperid"].ToString(),
                            CompanyName = rdr.Tables[0].Rows[i]["companyname"].ToString(),
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
