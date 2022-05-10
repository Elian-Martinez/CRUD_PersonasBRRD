using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUD_PersonBRRD.Models;
using CRUD_PersonBRRD.Models.ViewModels;
namespace CRUD_PersonBRRD.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult Index()
        {
            List<PersonViewModel> Person;
            using (TESTEntities Test=new TESTEntities())
            {

               Person = (from d in Test.People
                              select new PersonViewModel
                              {
                                  Id = d.Id,
                                  Name = d.Name,
                                  DateOfBirth = (DateTime)d.Date_of_birth
                              }).ToList();
            }
            return View(Person);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(PersonViewModel person)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (TESTEntities Test=new TESTEntities())
                    {
                        var _person = new PERSON();
                        _person.Name = person.Name;
                        _person.Date_of_birth = person.DateOfBirth;

                        Test.People.Add(_person);
                        Test.SaveChanges();
                    }
                    return Redirect("/Person/Index");
                }
                return View(person);
            }
            catch( Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Update(int Id)
        {
            PersonViewModel _person = new PersonViewModel();
            using (TESTEntities Test = new TESTEntities())
            {
                var _test = Test.People.Find(Id);
                _person.Id = _test.Id;
                _person.Name = _test.Name;
                _person.DateOfBirth = (DateTime)_test.Date_of_birth;
            }

            return View(_person);
        }
        [HttpPost]
        public ActionResult Update(PersonViewModel person)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (TESTEntities Test = new TESTEntities())
                    {
                        var _test = Test.People.Find(person.Id);
                        _test.Name = person.Name;
                        _test.Date_of_birth = person.DateOfBirth;

                        Test.Entry(_test).State=System.Data.Entity.EntityState.Modified;
                        Test.SaveChanges();
                    }
                    return Redirect("/Person/Index");
                }
                return View(person);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            using (TESTEntities Test = new TESTEntities())
            {
                var _test = Test.People.Find(Id);
                Test.People.Remove(_test);
                Test.SaveChanges();
            }

            return Redirect("/Person/Index");
            
        }
    }
}