using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI_tema2
{
   public class Node
    {
       String label; 	//numele nodului
       String type;	//subiect, obiect
       List<Link> links=new List<Link>();	//colectie ce retine muchiile (tip Link) pentru nodul respectiv 

       public void SetLabel(String x)
       {
           this.label = x;
       }

       public String GetLabel()
       {
           return this.label;
       }

       public void SetType(String x)
       {
           this.type = x;
       }

       public String GetType()
       {
           return this.type;
       }

       public void AddLink(Link xx)
       {
           this.links.Add(xx);
       }

       public List<Link> GetLinks()
       {
           return links;
       }
    }
}
