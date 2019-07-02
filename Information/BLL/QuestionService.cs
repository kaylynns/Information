using DAO;
using Entity;
using IBLL;
using IDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class QuestionService: IQuestionBll
    {
        IQuestionDao imd = IocContainer.IocCreate.CreateAll<QuestionDao>("QuestionOne", "QuestionDao");
        public List<info_Question> SelectAll()
        {
            return imd.SelectAll();
        }

        public List<info_Question> FenYe(Expression<Func<info_Question, int>> order, Expression<Func<info_Question, bool>> where, out int rows, int currentPage, int pageSize)
        {
            return imd.FenYe(order, where, out rows, currentPage, pageSize);
        }
        public int Add(info_Question t)
        {
            return imd.Add(t);
        }

        public int Update(info_Question t)
        {
            return imd.Update(t);
        }


        public List<info_Question> SelectWhere(Expression<Func<info_Question, bool>> where)
        {
            return imd.SelectWhere(where);
        }

        public int Delete(info_Question a)
        {
            return imd.Delete(a);
        }
    }
}
