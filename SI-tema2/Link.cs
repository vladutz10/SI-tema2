using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI_tema2
{
   public class Link
    {
        String type;	//in, out
	    String rights;	//drepturile de pe muchia respectiva
        Node node;	//nodul (tip Node) de la celalalt capat al muchiei 

       public void SetType(String x)
       {
            this.type=x;
       }

       public String GetType() 
       {
           return this.type;
       }

       public void SetRights(String x)
       {
           this.rights = x;
       }

       public String GetRights()
       {
           return this.rights;
       }

       public void SetNode(Node x)
       {
           this.node = x;
       }

       public Node GetNode()
       {
           return this.node;
       }

    }
}
