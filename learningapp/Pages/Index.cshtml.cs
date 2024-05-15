using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace learningapp.Pages;

public class IndexModel : PageModel
{
    public List<Course> Courses = new List<Course>();
    private readonly ILogger<IndexModel> _logger;
    private IConfiguration _configuration;
    public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<IActionResult> OnGet()
    {
        string functionUrl = "https://appfunction454545.azurewebsites.net/api/sqltrigger";
        using (HttpClient client = new HttpClient())
        {
            var response = await client.GetAsync(functionUrl);

            var data = await response.Content.ReadAsStringAsync();
            Courses = JsonConvert.DeserializeObject<List<Course>>(data);
            return Page();

        }

    }
}
