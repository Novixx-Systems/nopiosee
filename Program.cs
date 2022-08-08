//Nopiosee Language Translator (NPLT)
//Copyright (C) 2022  Novixx Systems

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <https://www.gnu.org/licenses/>.







/*
 * 
 * grammar:
 * When there is a multiple-version of your word (for example person and persons)
 * always use the 'e' character after the word (for example pasas and pasase)
 * 
 * If a line (not a sentence) ends with '?', start the line with '¿'
 * 
 */







using System.Text;
using System.Text.RegularExpressions;

namespace Nopiosee
{
    static class Program
    {
        static string which = null;

        /// <summary>
        /// Before adding a new word, you should check if it does not exist yet,
        /// and you should make sure it is ok.
        /// </summary>
        static Dictionary<string, string> m = new Dictionary<string, string>();
        public static String ReplaceWholeWord(this String s, String word, String bywhat)
        {
            char firstLetter = word[0];
            StringBuilder sb = new StringBuilder();
            bool previousWasLetterOrDigit = false;
            int i = 0;
            while (i < s.Length - word.Length + 1)
            {
                bool wordFound = false;
                char c = s[i];
                if (c == firstLetter)
                    if (!previousWasLetterOrDigit)
                        if (s.Substring(i, word.Length).Equals(word))
                        {
                            wordFound = true;
                            bool wholeWordFound = true;
                            if (s.Length > i + word.Length)
                            {
                                if (Char.IsLetterOrDigit(s[i + word.Length]) || s[i + word.Length] == '\'')
                                    wholeWordFound = false;
                            }

                            if (wholeWordFound)
                                sb.Append(bywhat);
                            else
                                sb.Append(word);

                            i += word.Length;
                        }

                if (!wordFound)
                {
                    previousWasLetterOrDigit = Char.IsLetterOrDigit(c);
                    sb.Append(c);
                    i++;
                }
            }

            if (s.Length - i > 0)
                sb.Append(s.Substring(i));


            return sb.ToString();
        }
        static void Init()
        {
            // Misc
            m.Add("want ", " je");
            m.Add("i first ", " solo");
            m.Add("first ", " beginn");
            m.Add("remove ", " da unis");
            m.Add("the ", " da");
            m.Add("right at ", " racht e");
            m.Add("of ", " qae");
            m.Add("to ", " y");
            m.Add("and ", " en");
            m.Add("a ", " a");
            m.Add("in ", " co");
            m.Add("is ", " equ");
            m.Add("it ", " cal");
            m.Add("you ", " master");
            m.Add("that ", " jet");
            m.Add("that is a ", " jet equ ze");
            m.Add("he ", " hi");
            m.Add("was ", " eror");
            m.Add("for ", " ta");
            m.Add("on ", " on");
            m.Add("are ", " yajo");
            m.Add("with ", " imusing");
            m.Add("as ", " assoc");
            m.Add("i ", " mei");
            m.Add("his ", " his");
            m.Add("they ", " jar");
            m.Add("be ", " had");
            m.Add("at ", " tocix");
            m.Add("one ", " zao");
            m.Add("have ", " ic");
            m.Add("this ", " zae");
            m.Add("from ", " freind");
            m.Add("or ", " of");
            m.Add("had ", " yeq");
            m.Add("by ", " err");
            m.Add("not ", " nouhva");
            m.Add("word ", " klas");
            m.Add("but ", " exe");
            m.Add("what ", " asqe");
            m.Add("some ", " tu");
            m.Add("we ", " qu");
            m.Add("can ", " sa");
            m.Add("out ", " nin");
            m.Add("other ", " oss");
            m.Add("were ", " user");
            m.Add("all ", " verwij");
            m.Add("there ", " tyhre");
            m.Add("when ", " whan");
            m.Add("up ", " diru");
            m.Add("use ", " gess");
            m.Add("your ", " yar");
            m.Add("how ", " toh");
            m.Add("said ", " talned");
            m.Add("talk ", " talne");
            m.Add("poop ", " buopu");
            m.Add("output ", " opu");
            m.Add("welcome ", " wyhere");
            m.Add("cool ", " yhas");
            m.Add("apple ", " appel");
            m.Add("water ", " holdon");
            m.Add("lava ", " lava");
            m.Add("easy ", " makli");
            m.Add("dance ", " dans");
            m.Add("dancing ", " dansing");
            m.Add("fire ", " fair");
            m.Add("delete ", " da unis");
            m.Add("removed ", " da unise");
            m.Add("deleted ", " da unise");
            m.Add("nothing ", " unis");
            m.Add("name ", " nym");
            m.Add("tree ", " bot");
            m.Add("grass ", " grous");
            m.Add("house ", " hisj");
            m.Add("forgot ", " forged");
            m.Add("forget ", " forge");
            m.Add("sure ", " zurr");
            m.Add("going ", " gutil");
            m.Add("if ", " wha");
            m.Add("yes ", " ja");
            m.Add("no ", " ne");
            m.Add("compare ", " samm");
            m.Add("two ", " tho");
            m.Add("three ", " grettho");
            m.Add("great ", " gret");
            m.Add("four ", " fortt");
            m.Add("five ", " faif");
            m.Add("six ", " syzz");
            m.Add("seven ", " savn");
            m.Add("eight ", " rofin");
            m.Add("nine ", " nei");
            m.Add("ten ", " thijn");
            m.Add("eleven ", " eddev");
            m.Add("hundred thousand ", " superthijn");
            m.Add("hundred ", " exthijn");
            m.Add("thousand ", " exexthijn");
            m.Add("million ", " milleon");
            m.Add("billion ", " billeon");
            m.Add("trillion ", " trilleon");
            m.Add("quadrillion ", " quadrilleon");
            m.Add("quintillion ", " quintilleon");
            m.Add("crazy ", " amazab");
            m.Add("hello ", " henlo");
            m.Add("heart ", " corzo");
            m.Add("home ", " hisj");
            m.Add("new ", " nieuw");
            m.Add("key ", " top");
            m.Add("board ", " bord");
            m.Add("keyboard ", " topbord");
            m.Add("universe ", " univars");
            m.Add("dog ", " dag");
            m.Add("cat ", " kat");
            m.Add("hey ", " henlo");
            m.Add("maybe ", " mischen");
            m.Add("still ", " toc");
            m.Add("right ", " rach");
            m.Add("okay ", " ok");
            m.Add("cold ", " kuod");
            m.Add("hot ", " het");
            m.Add("warm ", " heet");
            m.Add("gold ", " geite");
            m.Add("huge ", " big");
            m.Add("monster ", " monstre");
            m.Add("quad ", " mik");
            m.Add("just ", " jaqq");
            m.Add("about ", " gon");
            m.Add("version ", " versiy");
            m.Add("need ", " nee");
            m.Add("permission ", " permissie");
            m.Add("close ", " le disa");
            m.Add("continue ", " le goto");
            m.Add("cannot ", " sa nouhva");
            m.Add("valid ", " valide");
            m.Add("save ", " oplisn");
            m.Add("calculate ", " calcula");
            m.Add("calculator ", " calcular");
            m.Add("simple ", " samje");
            m.Add("number ", " digiv");
            m.Add("control ", " centre");
            m.Add("controller ", " centrell");
            m.Add("press ", " ola");
            m.Add("letter ", " latt");
            m.Add("letters ", " latts");
            m.Add("characters ", " chars");
            m.Add("tried ", " tryd");
            m.Add("look ", " mira");
            m.Add("question ", " pasas");
            m.Add("questions ", " pasase");
            m.Add("any ", " qe");
            m.Add("love ", " corzobe");
            m.Add("slow ", " calow");
            m.Add("fast ", " cahi");
            m.Add("quick ", " cahi");
            m.Add("sex ", " fakka");
            m.Add("today ", " vandahg");
            m.Add("too ", " ys");
            m.Add("rule ", " gahhr");
            m.Add("rules ", " gahhre");
            m.Add("stranger ", " string");
            m.Add("strangers ", " stringe");
            m.Add("never ", " novva");
            m.Add("give ", " gyvv");
            m.Add("bye ", " varwell");
            m.Add("goodbye ", " varwell");
            m.Add("been ", " bin");
            m.Add("know ", " no");
            m.Add("around ", " ax");
            m.Add("full ", " maxi");
            m.Add("holiday ", " nicedahg");
            m.Add("day ", " dahg");
            m.Add("write ", " wrait");
            m.Add("writes ", " wraite");
            m.Add("doing ", " doon");
            m.Add("really ", " looto");
            m.Add("like ", " loo");
            m.Add("words ", " klasse");
            m.Add("idiot ", " idioot");
            m.Add("idiots ", " idioote");
            m.Add("language ", " taal");
            m.Add("languages ", " taale");
            m.Add("hate ", " haates");
            m.Add("hates ", " haatese");
            m.Add("without ", " zondre");
            m.Add("amount ", " sizo");
            m.Add("someone ", " tuian");
            m.Add("something ", " tuess");
            m.Add("ask ", " qesa");
            m.Add("asking ", " qesae");
            m.Add("which ", " welk");
            m.Add("used ", " gesse");
            m.Add("using ", " gessene");
            m.Add("longer ", " lengre");
            m.Add("invalid ", " incrate");
            m.Add("recoverable ", " regetabel");
            m.Add("unrecoverable ", " inregetabel");
            m.Add("protection ", " vededige");
            m.Add("protect ", " vededig");
            m.Add("unrecognized", " inrecognized");
            m.Add("must", " mus");
            m.Add("present ", " pressena");
            m.Add("system ", " systeem");
            m.Add("error ", " fatle");
            m.Add("fatal ", " fettele");
            m.Add("table ", " tabel");
            m.Add("tables ", " tabele");
            // Very epic english shortcuts
            m.Add("it's ", " cal equ");
            m.Add("i'm ", " mei equ");
            m.Add("don't ", " do nouhva");
            m.Add("you're ", " master yajo");
        }
        static void Main(string[] args)
        {
            var str = "";
            if (args.Length > 0)
            {
                Init();
                if (System.IO.File.Exists(args[0]))
                {
                    str = " " + System.IO.File.ReadAllText(args[0]) + " ";
                    foreach (string line in m.Keys)
                    {
                        str = str.ToLower();
                        str = str.ReplaceWholeWord(line.Trim(), m[line].Trim());
                        str = Regex.Replace(str, " {2,}", " ");
                    }
                    Console.WriteLine(str);
                }
                Environment.Exit(0);
            }
            if (which == null)
            {
                Init();
                Console.WriteLine("Nopiosee Translator Tool\n");
                while (which != "1" && which != "2")
                {
                    which = Console.ReadLine() + "";
                }
            }
            if (which == "1")
            {
                str = " " + Console.ReadLine() + " ";
                foreach (string line in m.Keys)
                {
                    str = str.ReplaceWholeWord(m[line].Trim(), line.Trim());
                    str = str.Replace(" ?", "?");
                    str = str.Replace(" !", "!");
                    str = str.Replace(" .", ".");
                    str = str.Replace("¿ ", "");
                    str = str.Replace(" ,", ",");
                    str = Regex.Replace(str, " {2,}", " ");
                }
                Console.WriteLine("-> " + str);
            }
            else
            {
                str = " " + Console.ReadLine() + " ";
                foreach (string line in m.Keys)
                {
                    str = str.ToLower();
                    str = str.ReplaceWholeWord(line.Trim(), m[line].Trim());


                    str = Regex.Replace(str, " {2,}", " ");
                }
                Console.WriteLine("-> " + (str.EndsWith("? ") ? "¿" + str.Substring(1) : str));
            }
            Main(args);
        }
    }
}
