using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Module4_Test
{
    public class LazyLoadingSamples
    {
        private readonly ApplicationContext _context;

        public LazyLoadingSamples(ApplicationContext context)
        {
            _context = context;
        }

        /*public async Task Query1()
        {
            var employees = await _context.Employees
                .Select(x => new { Name = $"{x.FirstName} {x.LastName}", Title = x.Title.Name, Location = x.Office.Location })
                .ToListAsync();

            Console.WriteLine("Employee data");
            foreach (var employee in employees)
            {
                Console.WriteLine($"Employee name: {employee.Name}. Employee job title: {employee.Title}. Employee location: {employee.Location}.");
            }
        }*/
    }
}
