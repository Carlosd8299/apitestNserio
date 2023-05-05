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
    public class EmployeesRepository : SqlServerBase<Employees>, IEmployeesRepository
    {
        private readonly string _spGetEmployees;
        public EmployeesRepository(
        IOptions<InfraestructureSettings> settings
         ) : base(settings.Value.SqlSettings.ConnectionString)
        {
            _spGetEmployees = settings.Value.SqlSettings.SpGetEmployees;
        }

        public async Task<List<GetEmployeesResponseDto>> GetEmployees()
        {
            List<GetEmployeesResponseDto> response = new();
            try
            {
                using (var rdr = await ExecuteSpResults(_spGetEmployees))
                {
                    for (int i = 0; i < rdr.Tables[0].Rows.Count; i++)
                    {
                        var item = new GetEmployeesResponseDto
                        {
                            EmployeeId = rdr.Tables[0].Rows[i]["empid"].ToString(),
                            FullName = rdr.Tables[0].Rows[i]["fullname"].ToString(),
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
