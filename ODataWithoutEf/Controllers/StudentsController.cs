using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ODataWithoutEf.Models;

namespace ODataWithoutEf.Controllers;

public class StudentsController : ODataController
{
    private readonly SqlContext _sqlContext = new();

    [EnableQuery]
    public IEnumerable<Student> Get()
    {
        return _sqlContext.Get();
    }

    [EnableQuery]
    [HttpGet(nameof(GetById))]
    public IEnumerable<Student> GetById(int id)
    {
        var result = _sqlContext.Get().Where(model => model.Id == id);
        return result;
    }

    [EnableQuery]
    [HttpPost]
    public void Post([FromBody] Student obj)
    {
        if (ModelState.IsValid) _sqlContext.Add(obj);
    }

    [EnableQuery]
    [HttpPut("{id:int}")]
    public void Put(int id, [FromBody] Student obj)
    {
        if (ModelState.IsValid) _sqlContext.Update(id, obj);
    }

    [EnableQuery]
    [HttpDelete("{id:int}")]
    public void Delete(int id)
    {
        if (ModelState.IsValid) _sqlContext.Delete(id);
    }
}