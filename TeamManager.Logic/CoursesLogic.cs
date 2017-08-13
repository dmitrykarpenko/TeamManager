using TeamManager.DataLayer.Abstract;
using TeamManager.Model.Entities;
using TeamManager.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TeamManager.Logic
{
    public class CoursesLogic
    {
        private IUnitOfWork _unitOfWork;

        public CoursesLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Course> GetCourses(Expression<Func<Course, bool>> filter = null, PageInf pageInf = null,
                                              Expression<Func<Course, object>> orderBy = null, bool byDesc = false)
        {
            var coursesRepo = _unitOfWork.GetRepositiry<Course>();

            var courses = coursesRepo.Get(filter, pageInf, c => c.Teams, orderBy, byDesc);

            return courses;
        }

        public virtual IEnumerable<Course> InsertOrUpdate(IEnumerable<Course> courses)
        {
            var coursesRepo = _unitOfWork.GetRepositiry<Course>();
            var insOrUpdCourses = coursesRepo.InsertOrUpdate(courses);
            _unitOfWork.Save();

            return insOrUpdCourses;
        }

        public virtual IEnumerable<Course> Delete(int courseId)
        {
            var coursesRepo = _unitOfWork.GetRepositiry<Course>();
            var retCourses = coursesRepo.Delete(courseId);
            _unitOfWork.Save();

            return retCourses;
        }
    }
}
