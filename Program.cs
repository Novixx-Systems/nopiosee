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
        static string m = "want - je\ni first - solo\nfirst - beginn\nremove - da unis\nthe - da\nright at - racht e\nof - qae\nto - y\nand - en\na - a\nin - co\nis - equ\nit - cal\nyou - master\nthat - jet\nthat is a - jet equ ze\nhe - hi\nwas - eror\nfor - ta\non - on\nare - yajo\nwith - imusing\nas - assoc\ni - mei\nhis - his\nthey - jar\nbe - had\nat - tocix\none - zao\nhave - ic\nthis - zae\nfrom - freind\nor - of\nhad - yeq\nby - err\nnot - nouhva\nword - klas\nbut - exe\nwhat - asqe\nsome - tu\nwe - qu\ncan - sa\nout - nin\nother - oss\nwere - user\nall - verwij\nthere - tyhre\nwhen - whan\nup - diru\nuse - gess\nyour - yar\nhow - toh\nsaid - talned\ntalk - talne\npoop - buopu\noutput - opu\nwelcome - wyhere\ncool - yhas\napple - appel\nwater - holdon\nlava - lava\neasy - makli\ndance - dans\ndancing - dansing\nfire - fair\ndelete - da unis\nremoved - da unic\nnothing - unis\nname - nym\ntree - bot\ngrass - grous\nhouse - hisj\nforgot - forged\nforget - forge\nsure - zurr\ngoing - gutil\nif - wha\nyes - ja\nno - ne\ncompare - samm\ntwo - tho\nthree - grettho\ngreat - gret\nfour - fortt\nfive - faif\nsix - syzz\nseven - savn\neight - rofin\nnine - nei\nten - thijn\neleven - eddev\nhundred thousand - superthijn\nhundred - exthijn\nthousand - exexthijn\nmillion - milleon\nbillion - billeon\ntrillion - trilleon\nquadrillion - quadrilleon\nquintillion - quintilleon\ncrazy - amazab\nhello - henlo\nheart - corzo\nhome - hisj\nnew - nieuw\nkey - top\nboard - bord\nkeyboard - topbord\nuniverse - univars\ndog - dag\ncat - kat\nhey - henlo\nmaybe - mischen\nstill - toc\nright - rach\nokay - ok\ncold - kuod\nhot - het\nwarm - heet\ngold - geite\nhuge - big\nmonster - monstre\nquad - mik\njust - jaqq\nabout - gon\nversion - versiy\nneed - nee\npermission - permissie\nclose - le disa\ncontinue - le goto\ncannot - sa nouhva\nvalid - valide\nsave - oplisn\ncalculate - calcula\ncalculator - calcular\nsimple - samje\nnumber - digiv\ncontrol - centre\ncontroller - centrell\npress - ola\nletter - latt\nletters - latts\ncharacters - chars\ntried - tryd\nlook - mira\nquestion - pasas\nquestions - pasaos\nany - qe\nlove - corzobe\nslow - calow\nfast - cahi\nquick - cahi\nsex - fakka\ntoday - vandahg\ntoo - ys\nrule - gahhr\nrules - gahhrs\nstranger - string\nstrangers - strings\nnever - novva\ngive - gyvv\nbye - varwell\ngoodbye - varwell\nbeen - bin\nknow - no\naround - ax\nfull - maxi\nholiday - nicedahg\nday - dahg\nwrite - wrait\ndoing - doon\nreally - looto\nlike - loo\nwords - klasse\nidiot - idioot\nidiots - idioots\n";
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
                                if (Char.IsLetterOrDigit(s[i + word.Length]))
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
        static void Main(string[] args)
        {
            var str = "";
            if (args.Length > 0)
            {
                if (System.IO.File.Exists(args[0]))
                {
                    str = " " + System.IO.File.ReadAllText(args[0]) + " ";
                    foreach (string line in m.Trim().Split('\n'))
                    {
                        str = str.ToLower();
                        str = str.ReplaceWholeWord(line.Split("-".ToCharArray())[0].Trim(), line.Split("-".ToCharArray())[1].Trim());
                        str = Regex.Replace(str, " {2,}", " ");
                    }
                    Console.WriteLine(str);
                }
                Environment.Exit(0);
            }
            if (which == null)
            {
                Console.WriteLine("Nopiosee Translator Tool\n");
                while (which != "1" && which != "2")
                {
                    which = Console.ReadLine() + "";
                }
            }
            if (which == "1")
            {
                str = " " + Console.ReadLine() + " ";
                foreach (string line in m.Trim().Split('\n'))
                {
                    str = str.ReplaceWholeWord(line.Split("-".ToCharArray())[1].Trim(), line.Split("-".ToCharArray())[0].Trim());
                    str = str.Replace(" ?", "?");
                    str = str.Replace(" !", "!?");
                    str = str.Replace(" .", ".");
                    str = str.Replace(" ,", ",");
                    str = Regex.Replace(str, " {2,}", " ");
                }
            }
            else
            {
                str = " " + Console.ReadLine() + " ";
                foreach (string line in m.Trim().Split('\n'))
                {
                    str = str.ToLower();
                    str = str.ReplaceWholeWord(line.Split("-".ToCharArray())[0].Trim(), line.Split("-".ToCharArray())[1].Trim());
                    str = Regex.Replace(str, " {2,}", " ");
                }
            }
            Console.WriteLine("-> " + str);
            Main(args);
        }
    }
}
