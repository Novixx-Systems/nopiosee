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
 * If a sentence ends with '?', start the line with '¿'
 * 
 */







using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpeechLib;

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
            // Very epic english shortcuts
            m.Add("it's ", " it is");
            m.Add("its ", " it is");
            m.Add("thats ", " that is");
            m.Add("that's ", " that is");
            m.Add("i'm ", " i am");
            m.Add("aren't ", " not");
            m.Add("im ", " i am");
            m.Add("don't ", " do not");
            m.Add("dont ", " do not");
            m.Add("wanna ", " want to");
            m.Add("you're ", " you are");
            m.Add("gotta ", " need to");
            // Words that are easy or whatever you call them I am too stupid to know
            m.Add("please ", " ta mei play");
            m.Add("i want ", " paso");
            m.Add("what is ", " dimi");
            m.Add("is dumb ", " equ domelementa");
            m.Add("is cool ", " equ yhasa");
            m.Add("is crazy ", " equ amaza");
            m.Add("am free ", " am outo");
            m.Add("you are free ", " un q outo");
            m.Add("are you free ", " qi outo");
            m.Add("he is free ", " hi equ outo");
            m.Add("she is free ", " shi equ outo");
            m.Add("free ", " grathis");
            // Misc
            m.Add("you have ", " masteqe");
            m.Add("kidding ", " qavoe");
            m.Add("want ", " je");
            m.Add("are you ", " qi");
            m.Add("i first ", " solo");
            m.Add("first ", " beginn");
            m.Add("remove ", " da unis");
            m.Add("the ", " da");
            m.Add("right at ", " rach e");
            m.Add("of ", " qae");
            m.Add("to ", " y");
            m.Add("and ", " en");
            m.Add("a ", " a");
            m.Add("in ", " co");
            m.Add("is ", " equ");
            m.Add("it ", " cal");
            m.Add("you ", " un");
            m.Add("that ", " jet");
            m.Add("that is a ", " jet equ ze");
            m.Add("he ", " hi");
            m.Add("she ", " shi");
            m.Add("was ", " eror");
            m.Add("for ", " ta");
            m.Add("on ", " on");
            m.Add("with ", " imusing");
            m.Add("as ", " assoc");
            m.Add("i ", " mei");
            m.Add("are ", " q");
            m.Add("his ", " his");
            m.Add("her ", " shis");
            m.Add("they ", " jar");
            m.Add("be ", " had");
            m.Add("at ", " tocix");
            m.Add("one ", " zao");
            m.Add("have ", " ic");
            m.Add("this ", " zae");
            m.Add("from ", " freind");
            m.Add("or ", " que");
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
            m.Add("removes ", " da unise");
            m.Add("deleted ", " da unise");
            m.Add("deletes ", " da unise");
            m.Add("nothing ", " unis");
            m.Add("name ", " nym");
            m.Add("tree ", " bot");
            m.Add("grass ", " grous");
            m.Add("house ", " hisj");
            m.Add("forgot ", " forged");
            m.Add("forget ", " forge");
            m.Add("sure ", " yos");
            m.Add("going ", " gutil");
            m.Add("if ", " wha");
            m.Add("yes ", " ja");
            m.Add("know ", " nó");
            m.Add("no ", " ne");
            m.Add("compare ", " samm");
            m.Add("two ", " tho");
            m.Add("three ", " grettho");
            m.Add("great ", " gret");
            m.Add("joke ", " qavo");
            m.Add("jokes ", " qeve");
            m.Add("joking ", " qave");
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
            m.Add("crazy ", " amaz");
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
            m.Add("just ", " jaq");
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
            m.Add("simple ", " simpel");
            m.Add("number ", " digi");
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
            m.Add("my ", " ya");
            m.Add("feel ", " be");
            m.Add("feeling ", " ebe");
            m.Add("eat ", " eet");
            m.Add("eats ", " eete");
            m.Add("eating ", " eete");
            m.Add("baby ", " chel");
            m.Add("child ", " chella");
            m.Add("drink ", " eer");
            m.Add("drinks ", " eere");
            m.Add("drinking ", " eere");
            m.Add("cake ", " keek");
            m.Add("more ", " mer");
            m.Add("attempt ", " at");
            m.Add("file ", " exto");
            m.Add("folder ", " map");
            m.Add("directory ", " map");
            m.Add("specify ", " spec");
            m.Add("specified ", " spec");
            m.Add("specific ", " spece");
            m.Add("specifies ", " speca");
            m.Add("type ", " tik");
            m.Add("types ", " tike");
            m.Add("typing ", " tike");
            m.Add("text ", " tekst");
            m.Add("texting ", " tekste");
            m.Add("texted ", " tekste");
            m.Add("change ", " chas");
            m.Add("changing ", " chase");
            m.Add("changed ", " chase");
            m.Add("program ", " programme");
            m.Add("programming ", " program");
            m.Add("screen ", " scherm");
            m.Add("following ", " nekst");
            m.Add("follow ", " nek");
            m.Add("special ", " speciaalen");
            m.Add("default ", " normale");
            m.Add("setting ", " instan");
            m.Add("settings ", " instane");
            m.Add("now ", " nu");
            m.Add("current ", " now");
            m.Add("currently ", " nowe");
            m.Add("displays ", " monitere");
            m.Add("display ", " moniter");
            m.Add("because ", " omdat");
            m.Add("either ", " az");
            m.Add("missing ", " unfin");
            m.Add("tell ", " como");
            m.Add("gay ", " homo");
            m.Add("gays ", " homoe");
            m.Add("so ", " soy");
            m.Add("music ", " musiek");
            m.Add("ball ", " bal");
            m.Add("difficult ", " difficil");
            m.Add("difficulty ", " difficile");
            m.Add("night ", " yano");
            m.Add("tonight ", " noyano");
            m.Add("boy ", " nano");
            m.Add("girl ", " nani");
            m.Add("man ", " grano");
            m.Add("woman ", " grani");
            m.Add("dumb ", " domelement");
            m.Add("then ", " es");
            m.Add("buy ", " perche");
            m.Add("purchase ", " perche");
            m.Add("purchased ", " eperche");
            m.Add("bought ", " eperche");
            m.Add("master ", " mess");
            m.Add("teacher ", " teclar");
            m.Add("teachers ", " teclare");
            m.Add("toilet ", " toilette");
            m.Add("go ", " goto");
            m.Add("amazing ", " hanya");
            m.Add("amazed ", " hanyae");
            m.Add("boss ", " berfung");
            m.Add("bosses ", " berfunge");
            m.Add("thank un ", " berfussi");
            m.Add("thanks ", " berfussi");
            m.Add("imagination ", " imaginacíon");
            m.Add("translate ", " transacíon");
            m.Add("translated ", " transacíone");
            m.Add("game ", " spell");
            m.Add("games ", " spelle");
            m.Add("gaming ", " spella");
            m.Add("abbreviation ", " simpelecíon");
            m.Add("abbreviations ", " simpelecíone");
            m.Add("animal ", " anymel");
            m.Add("animals ", " anymele");
            m.Add("cute ", " hox");
            m.Add("experience ", " pengelemen");
            m.Add("experiences ", " pengelemene");
            m.Add("experiencing ", " pengelemena");
            m.Add("command ", " komander");
            m.Add("commands ", " komandere");
            m.Add("commanding ", " komandera");
            m.Add("computer ", " computadoll");
            m.Add("computers ", " computadolle");
            m.Add("computing ", " computadolla");
            m.Add("normal ", " okal");
            m.Add("normally ", " okale");
            m.Add("certain ", " tu");
            m.Add("applications ", " appecíone");
            m.Add("application ", " appecíon");
            m.Add("apps ", " appe");
            m.Add("files ", " exto");
            m.Add("cooler ", " yhasa");
            m.Add("color ", " kolor");
            m.Add("colors ", " kolore");
            m.Add("black ", " zwart");
            m.Add("gray ", " greis");
            m.Add("white ", " whit");
            m.Add("purple ", " porpel");
            m.Add("green ", " groel");
            m.Add("lime ", " cimoen");
            m.Add("yellow ", " geel");
            m.Add("blue ", " bouw");
            m.Add("orange ", " sinas");
            m.Add("zero ", " null");
            m.Add("favorite ", " favoritto");
            m.Add("favorites ", " favorittoe");
            m.Add("start ", " beginne");
            m.Add("additional ", " additicíon");
            m.Add("task ", " tako");
            m.Add("tasks ", " takoe");
            m.Add("apples ", " appele");
            m.Add("easiest ", " makliest");
            m.Add("defaults ", " normale");
            m.Add("sun ", " zon");
            m.Add("under ", " onder");
            m.Add("same ", " zelde");
            m.Add("anyone ", " yian");
            m.Add("others ", " osse");
        }
        static bool IsAllUpper(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (!Char.IsUpper(input[i]))
                    return false;
            }

            return true;
        }
        static void Main(string[] args)
        {
            var str = "";
            if (args.Length > 0)
            {
                Init();
                if (args[0] == "res")
                {
                    if (System.IO.File.Exists(args[1]))
                    {
                        str = " " + System.IO.File.ReadAllText(args[1]) + " ";
                        str = str.Replace("\\r\\n", "*(%$%*($%^()($$*######");
                        foreach (string line in str.Split("\n"))
                        {
                            try
                            {
                                string newlp = line.Split("\"")[1];
                                string oldlp = line.Split("\"")[1];
                                newlp = newlp.ToLower();
                                foreach (string linae in m.Keys)
                                {
                                    newlp = newlp.ReplaceWholeWord(linae.Trim(), m[linae].Trim());
                                    newlp = Regex.Replace(newlp, " {2,}", " ");
                                    newlp = newlp.EndsWith("? ") ? "¿" + newlp.Substring(1) : newlp;
                                }
                                str = str.Replace(oldlp, newlp);
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                    str = str.Replace("*(%$%*($%^()($$*######", "\\r\\n");
                    Console.WriteLine(str);
                    Environment.Exit(0);
                }
                if (args[0] == "dic")
                {
                    int dicint = 0;
                    foreach (string line in m.Keys)
                    {
                        dicint++;
                        if (dicint < 30)
                        {
                            continue;
                        }
                        using var httpClient = new HttpClient();
                        var request = new HttpRequestMessage(HttpMethod.Get, "https://api.dictionaryapi.dev/api/v2/entries/en/" + line);
                        var response = httpClient.Send(request);
                        using var reader = new StreamReader(response.Content.ReadAsStream());
                        string responseBody = reader.ReadToEnd();
                        dynamic stuff = JsonConvert.DeserializeObject(responseBody);

                        foreach (var s in stuff)
                        {

                            Console.WriteLine(m[line] + "    ===   " + (string)s.SelectToken("meanings[0].definitions[0].definition"));
                            Console.WriteLine(m[line] + "    ===   " + (string)s.SelectToken("meanings[0].definitions[1].definition"));
                            Console.WriteLine(m[line] + "    ===   " + (string)s.SelectToken("meanings[0].definitions[2].definition"));
                            Console.WriteLine(m[line] + "    ===   " + (string)s.SelectToken("meanings[0].definitions[3].definition"));
                            Console.WriteLine(m[line] + "    ===   " + (string)s.SelectToken("meanings[0].definitions[4].definition"));
                            Console.WriteLine(m[line] + "    ===   " + (string)s.SelectToken("meanings[0].definitions[5].definition"));
                            Console.WriteLine(m[line] + "    ===   " + (string)s.SelectToken("meanings[0].definitions[6].definition"));
                            Console.WriteLine(m[line] + "    ===   " + (string)s.SelectToken("meanings[0].definitions[7].definition"));
                            break;
                        }
                    }
                    Environment.Exit(0);
                }
                if (System.IO.File.Exists(args[0]))
                {
                    str = " " + System.IO.File.ReadAllText(args[0]) + " ";
                    foreach (string line in m.Keys)
                    {
                        str = str.ToLower();
                        str = str.ReplaceWholeWord(line.Trim(), m[line].Trim());
                        str = Regex.Replace(str, " {2,}", " ");
                        str = str.EndsWith("? ") ? "¿" + str.Substring(1) : str;
                    }
                    Console.WriteLine(str);
                }
                Environment.Exit(0);
            }
            if (which == null)
            {
                Init();
                Console.WriteLine("Nopiosee Translator Tool (US English -> Nopiosee or vice versa)\n");
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
                    str = str.Replace(" ¿", "");
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
                ISpeechVoice voice = new SpVoice();
                voice.Rate = 1;
                voice.Volume = 100;
                voice.Speak("<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='en-US'>"
                            + str.Replace("1", "zao").Replace("0", "null").Replace("oss", "oz").Replace("qe", "k").Replace("le", "lah").Replace(" bal", " bel").Replace("dahg ", " do ").Replace("t", "j").Replace("ja", "ya").Replace("assoc", "assok") // pronounciation
                            + "</speak>",
                            SpeechVoiceSpeakFlags.SVSFlagsAsync | SpeechVoiceSpeakFlags.SVSFIsXML); ;
            }
            Main(args);
        }
    }
}
