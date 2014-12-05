using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI_tema2
{
    class Program
    {
        static Node nodS, nodSprim, nodPprim;
        static List<Node> noduriS = new List<Node>();
        static List<Node> noduriSprim = new List<Node>();
        static List<Node> noduriPprim = new List<Node>();

        public static void Main(string[] args)
        {

            Dictionary<String, Node> graf = new Dictionary<string, Node>();

            //construim pathul de intrare:
            string path = Directory.GetCurrentDirectory();
            path = System.IO.Directory.GetParent(path).FullName;
            path = System.IO.Directory.GetParent(path).FullName;
            path = path + @"\intrare.txt";
            var sr = new StreamReader(path);
            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '(', ')', '|' };
            string linie = sr.ReadLine();
            while (String.Compare((linie = sr.ReadLine()), ("Muchii:")) != 0)
            {
                Node x = new Node();
                string[] subStrings = linie.Split(delimiterChars);
                x.SetLabel(subStrings[0]);
                x.SetType(subStrings[1]);
                graf.Add(subStrings[0], x);
            }

            while ((linie = sr.ReadLine()) != null)
            {
                string[] subStrings = linie.Split(delimiterChars);
                Link x = new Link();
                x.SetType(subStrings[2]);
                x.SetRights(subStrings[3]);

                foreach (KeyValuePair<String, Node> item in graf)
                    if (String.Compare(item.Key, subStrings[0]) == 0)
                        foreach (KeyValuePair<String, Node> item2 in graf)
                            if (String.Compare(item2.Key, subStrings[4]) == 0)
                            {
                                x.SetNode(item2.Value);
                                item.Value.AddLink(x);
                            }
            }

            String input;
            Console.WriteLine("Dati predicatul can.share(r,x,p,G)");
            // input=Console.ReadLine();
            input = "r,F,A,G";
            Console.WriteLine("Conditie 1: " + conditie1(input, graf));

            Console.WriteLine("2.1 " + conditie21(input, graf));
            Console.WriteLine("2.2 " + conditie22(input, graf));
            Console.WriteLine("2.3 " + conditie23(input, graf));
            Console.WriteLine("2.4 " + conditie24(input, graf));

            /*
                         int ok = 0;

                         if (conditie21(input, graf))
                             if (conditie22(input, graf))
                                 if (conditie23(input, graf))
                                     if (conditie24(input, graf))
                                         ok = 1;

                         if (ok == 1) Console.WriteLine("Conditie 2: True");
                         else Console.WriteLine("Conditie 2: False");
                       
                        */
        }

        public static bool conditie1(String input, Dictionary<String, Node> graf)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '(', ')', '|' };
            string[] SubStrings = input.Split(delimiterChars);

            foreach (KeyValuePair<String, Node> item in graf)
                if (String.Compare(item.Key, SubStrings[2]) == 0)
                {
                    List<Link> nodeLinks = item.Value.GetLinks();
                    foreach (Link element in nodeLinks)
                        if (String.Compare(element.GetType(), "out") == 0)
                            if (String.Compare(element.GetRights(), SubStrings[0]) == 0)
                                if (String.Compare((element.GetNode()).GetLabel(), SubStrings[1]) == 0)
                                    return true;
                }
            return false;
        }

        public static bool conditie21(string input, Dictionary<String, Node> graf)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '(', ')', '|' };
            string[] SubStrings = input.Split(delimiterChars);
            int gasit = 0;

            foreach (KeyValuePair<String, Node> item in graf)
                if (String.Compare(item.Key, SubStrings[1]) == 0)
                {
                    Node s = item.Value;
                    List<Link> sLinks = s.GetLinks();
                    foreach (Link element in sLinks)
                        if (String.Compare(element.GetType(), "in") == 0 && element.GetRights().Contains(SubStrings[0]))
                        {
                            gasit = 1;
                            noduriS.Add(element.GetNode());
                            //nodS = element.GetNode();
                            //return true;
                        }
                }
            if (gasit == 1) return true;
            else return false;
        }

        public static bool conditie22(string input, Dictionary<String, Node> graf)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '(', ')', '|' };
            string[] SubStrings = input.Split(delimiterChars);
            int ok = 0, gasit = 0;

            /*   foreach (KeyValuePair<String, Node> item in graf)
                   if (String.Compare(item.Key, SubStrings[2]) == 0)
                       if (String.Compare(item.Value.GetType(), "subiect") == 0)
                       {
                           nodPprim = item.Value;
                           return true;
                       }
                       else
                       {
                           Node s = item.Value;
                           List<Link> sLinks = s.GetLinks();
                           foreach (Link element in sLinks)
                               if (String.Compare(element.GetType(), "in") == 0 && element.GetRights().Contains("g"))
                               {
                                   Node x = element.GetNode();
                                   while (String.Compare(x.GetType(), "subiect") != 0 && gasit == 1)
                                   {
                                       gasit = 0;
                                       List<Link> xLinks = x.GetLinks();
                                       foreach (Link myElement in xLinks)
                                           if (String.Compare(myElement.GetType(), "in") == 0 && myElement.GetRights().Contains("t"))
                                           { x = myElement.GetNode(); gasit = 1; }
                                       // return true;

                                   }
                                   if (String.Compare(x.GetType(), "subiect") == 0)
                                   {
                                       nodPprim = x;
                                       return true;
                                       ok = 1;
                                   }

                               }
                           //  return true;
                       }
               if (ok == 1) return true;
             */

            String vizitat = "";
            Stack<Node> viz = new Stack<Node>();
            int umblat = 1, grant = 0;

            foreach (KeyValuePair<String, Node> item in graf)
                if (String.Compare(item.Key, SubStrings[2]) == 0)
                {
                    if (String.Compare(item.Value.GetType(), "subiect") == 0)
                    {
                        //nodPprim = item.Value;
                        noduriPprim.Add(item.Value);
                        // return true;
                        gasit = 1;
                    }
                    //  else
                    // {


                    Node s = item.Value;
                    List<Link> sLinks = s.GetLinks();
                    vizitat += s.GetLabel();
                    viz.Push(s);

                    while (viz.Count() > 0)
                    {
                        umblat = 0;
                        Node x = viz.Peek();
                        if (viz.Count() == 1) grant = 0;
                        List<Link> xLinks = x.GetLinks();
                        foreach (Link myElement in x.GetLinks())
                            if (grant == 0 && String.Compare(myElement.GetType(), "in") == 0 && myElement.GetRights().Contains("g") && !vizitat.Contains(myElement.GetNode().GetLabel()))
                            {
                                vizitat += myElement.GetNode().GetLabel();
                                viz.Push(myElement.GetNode());
                                umblat = 1;
                                grant++;
                                if (String.Compare(myElement.GetNode().GetType(), "subiect") == 0 && (String.Compare(item.Key, SubStrings[2]) == 0))
                                {
                                    // nodPprim = myElement.GetNode();
                                    //return true;
                                    noduriPprim.Add(myElement.GetNode());
                                    gasit = 1;
                                    // break;
                                }
                                break;
                            }
                            else
                                if (String.Compare(myElement.GetType(), "in") == 0 && myElement.GetRights().Contains("t") && !vizitat.Contains(myElement.GetNode().GetLabel()))
                                {
                                    vizitat += myElement.GetNode().GetLabel();
                                    umblat = 1;
                                    viz.Push(myElement.GetNode());
                                    if (String.Compare(myElement.GetNode().GetType(), "subiect") == 0 && (String.Compare(item.Key, SubStrings[2]) == 0))
                                    {
                                        // nodPprim = myElement.GetNode();
                                        // return true;
                                        noduriPprim.Add(myElement.GetNode());
                                        gasit = 1;
                                    }

                                    break;
                                }
                        if (umblat == 0)
                            x = viz.Pop();
                    }
                }


            if (gasit == 1) return true;
            else return false;
        }

        public static bool conditie23(string input, Dictionary<String, Node> graf)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t', '(', ')', '|' };
            string[] SubStrings = input.Split(delimiterChars);
            int ok = 0, gasit = 0;

            /*      foreach (KeyValuePair<String, Node> item in graf)
                      if (String.Compare(item.Key, nodS.GetLabel()) == 0)
                          if (String.Compare(item.Value.GetType(), "subiect") == 0)
                          {
                              nodSprim = item.Value;
                              return true;
                          }
                          else
                          {
                              Node s = item.Value;
                              List<Link> sLinks = s.GetLinks();
                              foreach (Link element in sLinks)
                                  if (String.Compare(element.GetType(), "in") == 0 && element.GetRights().Contains("t"))
                                  {
                                      Node x = element.GetNode();
                                      if (String.Compare(x.GetType(), "subiect")==0) 
                                      {
                                          nodSprim = x;
                                          ok = 1;
                                      }
                                      else 
                                          while (String.Compare(x.GetType(), "subiect") != 0 && gasit == 1)
                                      {
                                          gasit = 0;
                                          List<Link> xLinks = x.GetLinks();
                                          foreach (Link myElement in xLinks)
                                              if (String.Compare(myElement.GetType(), "in") == 0 && myElement.GetRights().Contains("t"))
                                              { x = myElement.GetNode(); gasit = 1; }
                                          // return true;

                                      }

                                      if (String.Compare(x.GetType(), "subiect") == 0)
                                      {
                                          nodSprim = x;
                                          ok = 1;
                                      }

                                  }
                              //  return true;
                          }
                  if (ok == 1) return true;
             */



            String vizitat = "";
            Stack<Node> viz = new Stack<Node>();
            int umblat = 1, grant = 0;

            for (int i = 0; i < noduriS.Count(); i++)
            {

                foreach (KeyValuePair<String, Node> item in graf)
                    if (String.Compare(item.Key, noduriS[i].GetLabel()) == 0)
                    {
                        if (String.Compare(item.Value.GetType(), "subiect") == 0)
                        {
                            // nodSprim=item.Value;
                            // return true;
                            noduriSprim.Add(item.Value);
                            gasit = 1;
                        }
                        //   else
                        //  {
                        Node s = item.Value;
                        List<Link> sLinks = s.GetLinks();
                        vizitat += s.GetLabel();
                        viz.Push(s);

                        while (viz.Count() > 0)
                        {
                            umblat = 0;
                            Node x = viz.Peek();
                            if (viz.Count() == 1) grant = 0;
                            List<Link> xLinks = x.GetLinks();
                            foreach (Link myElement in x.GetLinks())
                                if (String.Compare(myElement.GetType(), "in") == 0 && myElement.GetRights().Contains("t") && !vizitat.Contains(myElement.GetNode().GetLabel()))
                                {
                                    vizitat += myElement.GetNode().GetLabel();
                                    umblat = 1;
                                    viz.Push(myElement.GetNode());
                                    if (String.Compare(myElement.GetNode().GetType(), "subiect") == 0)
                                    {
                                        noduriSprim.Add(myElement.GetNode());
                                        gasit = 1;
                                        // return true;
                                        //  break;
                                    }
                                    break;
                                }
                            if (umblat == 0)
                                x = viz.Pop();
                        }
                    }

            }

            if (gasit == 1) return true;
            else return false;
        }

        public static bool conditie24(string input, Dictionary<String, Node> graf)
        {
            String vizitat = "", lastmove = "";
            Stack<Node> viz = new Stack<Node>();
            int umblat = 1, ns = 0;
            int gi = 0, go = 0;
            /*          Node s = nodPprim;
                      List<Link> sLinks = s.GetLinks();
                      vizitat += s.GetLabel();
                      foreach (Link element in sLinks)
                         // if (String.Compare(element.GetType(), "out") == 0)
                              if( element.GetRights().Contains("t"))
                                  {
                                       
                                      Node x = element.GetNode();
                                      vizitat += x.GetLabel();
                                      while (String.Compare(x.GetLabel(), nodSprim.GetLabel()) != 0 && umblat==1)
                                      {
                                          umblat = 0;
                                          List<Link> xLinks = x.GetLinks();
                                          foreach (Link myElement in x.GetLinks())
                                           //   if (String.Compare(myElement.GetType(), "out") == 0)
                                                  if(myElement.GetRights().Contains("t") && !vizitat.Contains(myElement.GetNode().GetLabel()))
                                                  {
                                                      x = myElement.GetNode();
                                                      vizitat += x.GetLabel();
                                                      umblat = 1;
                                                      break;
                                                  }

                                                  else if (myElement.GetRights().Contains("g") && gi <= 1 && go <= 1 && !vizitat.Contains(myElement.GetNode().GetLabel()))
                                                  {

                                                      x = myElement.GetNode();
                                                      vizitat += x.GetLabel();
                                                      umblat = 1;
                                                      if (String.Compare(myElement.GetType(), "in") == 0)
                                                          gi++;
                                                      else go++;
                                                      break;
                                                  }
                
                                      }

                                      if (String.Compare(x.GetLabel(),nodPprim.GetLabel() ) == 0 && gi==1 && go==1)
                                      {
                                          return true;
                                      }

                                  }*/
            for (int i = 0; i < noduriPprim.Count(); i++)
                for (int j = 0; j < noduriSprim.Count(); j++)
                {
                    Node s = noduriPprim[i];
                    //Node s = nodPprim;
                    List<Link> sLinks = s.GetLinks();
                    vizitat += s.GetLabel();
                    viz.Push(s);

                    // while (String.Compare(viz.Peek().GetLabel(), nodSprim.GetLabel()) != 0 && viz.Count()>0)
                    while (viz.Count() > 0)
                    {
                        if (String.Compare(viz.Peek().GetLabel(), noduriSprim[j].GetLabel()) == 0) return true;
                        umblat = 0;
                        Node x = viz.Peek();
                        List<Link> xLinks = x.GetLinks();
                        foreach (Link myElement in x.GetLinks())
                            if (myElement.GetRights().Contains("t") && !vizitat.Contains(myElement.GetNode().GetLabel()))
                            {
                                vizitat += myElement.GetNode().GetLabel();
                                viz.Push(myElement.GetNode());
                                umblat = 1;
                                lastmove = "t";
                                break;
                            }
                            else if (myElement.GetRights().Contains("g") && !vizitat.Contains(myElement.GetNode().GetLabel()))
                                if (go <= 0 && String.Compare(myElement.GetType(), "out") == 0)
                                {
                                    vizitat += myElement.GetNode().GetLabel();
                                    umblat = 1;
                                    viz.Push(myElement.GetNode());
                                    go++;
                                    lastmove = "go";
                                }
                                else if (gi <= 0 && String.Compare(myElement.GetType(), "in") == 0 && go > 0)
                                {

                                    vizitat += myElement.GetNode().GetLabel();
                                    umblat = 1;
                                    viz.Push(myElement.GetNode());
                                    gi++;
                                    lastmove = "gi";
                                }
                        if (umblat == 0)
                        {
                            x = viz.Pop();
                            // x = viz.Pop();
                            if (lastmove.Contains("gi")) gi = 0;
                            else go = 0;
                            //  vizitat = vizitat.Remove(vizitat.Length - 1);
                        }
                    }



                    if (vizitat.Contains(noduriSprim[j].GetLabel()) && gi == 1 && go == 1)
                    {
                        return true;
                    }

                }


            return false;

        }



    }
}
