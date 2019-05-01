using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCbuild_ASP.MVC_.Domain.Entities
{
    public partial class Comparison<Ent>
    {
        private List<Ent> compareList= new List<Ent>();

        public void AddItem(Ent ent)
        {
            Ent item = compareList
                .Where(i => i.Equals(ent))
                .FirstOrDefault();
            if (item == null)
            {
                compareList.Add(ent);
            }
        }

        public void Remove(Ent ent)
        {
            compareList.RemoveAll(i => i.Equals(ent));
        }

        public void Clear()
        {
            compareList.Clear();
        }

        public IEnumerable<Ent> CompareList { get { return compareList; } }
    }
}