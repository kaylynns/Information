using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAO
{
  public  interface IQuestionDao
    {
        List<info_Question> SelectAll();
        List<info_Question> SelectWhere(Expression<Func<info_Question, bool>> where);
        List<info_Question> FenYe<K>(Expression<Func<info_Question, K>> order, Expression<Func<info_Question, bool>> where, out int rows, int currentPage, int pageSize);
        int Add(info_Question t);
        int Delete(info_Question t);
        int Update(info_Question t);
    }
}
