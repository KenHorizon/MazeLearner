using MazeLearner.Worlds;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.BattleSystems.Questions.English
{
    public class QuestionBuilder
    {
        public static void Register()
        {

            EnglishSubject.Add(EnglishQuestion.Create("Maria is my friend")
                .Type(EnglishType.Noun)
                .Tooltips0("A noun is the name of a person, place, animal, or thing")
                .A("is")
                .B("my")
                .C("Maria")
                .D("friend")
                .CorrectQuestion(2)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("The dog is barking")
                .Type(EnglishType.Noun)
                .Tooltips0("A noun is the name of a person, place, animal, or thing")
                .A("barking")
                .B("dog")
                .C("is")
                .D("the")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("I have a pencil")
                .Type(EnglishType.Noun)
                .Tooltips0("A noun is the name of a person, place, animal, or thing")
                .A("have")
                .B("pencil")
                .C("I")
                .D("a")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("The teacher is kind")
                .Type(EnglishType.Noun)
                .Tooltips0("A noun is the name of a person, place, animal, or thing")
                .A("kind")
                .B("is")
                .C("teacher")
                .D("The")
                .CorrectQuestion(2)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("We went to the park")
                .Type(EnglishType.Noun)
                .Tooltips0("A noun is the name of a person, place, animal, or thing")
                .A("went")
                .B("park")
                .C("We")
                .D("to")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("The cat is sleeping")
                .Type(EnglishType.Noun)
                .Tooltips0("A noun is the name of a person, place, animal, or thing")
                .A("sleeping")
                .B("cat")
                .C("is")
                .D("the")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("My mother is cooking")
                .Type(EnglishType.Noun)
                .Tooltips0("A noun is the name of a person, place, animal, or thing")
                .A("cooking")
                .B("mother")
                .C("is")
                .D("My")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("I see a bird")
                .Type(EnglishType.Noun)
                .Tooltips0("A noun is the name of a person, place, animal, or thing")
                .A("see")
                .B("bird")
                .C("I")
                .D("a")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("The car is fast")
                .Type(EnglishType.Noun)
                .Tooltips0("A noun is the name of a person, place, animal, or thing")
                .A("fast")
                .B("car")
                .C("is")
                .D("The")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("The baby is crying")
                .Type(EnglishType.Noun)
                .Tooltips0("A noun is the name of a person, place, animal, or thing")
                .A("baby")
                .B("crying")
                .C("is")
                .D("The")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("She is my sister.")
                .Type(EnglishType.Pronoun)
                .Tooltips0("A pronoun replaces a noun")
                .A("sister")
                .B("She")
                .C("is")
                .D("my")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("He is playing.")
                .Type(EnglishType.Pronoun)
                .Tooltips0("A pronoun replaces a noun")
                .A("He")
                .B("playing")
                .C("is")
                .D("none")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("They are happy.")
                .Type(EnglishType.Pronoun)
                .Tooltips0("A pronoun replaces a noun")
                .A("happy")
                .B("They")
                .C("are")
                .D("none")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("We are friends.")
                .Type(EnglishType.Pronoun)
                .Tooltips0("A pronoun replaces a noun")
                .A("friends")
                .B("We")
                .C("are")
                .D("none")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("It is raining.")
                .Type(EnglishType.Pronoun)
                .Tooltips0("A pronoun replaces a noun")
                .A("raining")
                .B("It")
                .C("is")
                .D("none")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("I am eating.")
                .Type(EnglishType.Pronoun)
                .Tooltips0("A pronoun replaces a noun")
                .A("eating")
                .B("I")
                .C("am")
                .D("none")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("You are kind.")
                .Type(EnglishType.Pronoun)
                .Tooltips0("A pronoun replaces a noun")
                .A("kind")
                .B("You")
                .C("are")
                .D("none")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("She is singing.")
                .Type(EnglishType.Pronoun)
                .Tooltips0("A pronoun replaces a noun")
                .A("singing")
                .B("She")
                .C("is")
                .D("none")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("They are running.")
                .Type(EnglishType.Pronoun)
                .Tooltips0("A pronoun replaces a noun")
                .A("running")
                .B("They")
                .C("are")
                .D("none")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("We are studying.")
                .Type(EnglishType.Pronoun)
                .Tooltips0("A pronoun replaces a noun")
                .A("studying")
                .B("We")
                .C("are")
                .D("none")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("The boy runs fast.")
                .Type(EnglishType.Verb)
                .A("boy")
                .B("runs")
                .C("fast")
                .D("the")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("She eats rice.")
                .Type(EnglishType.Verb)
                .Tooltips0("A verb is an action word.")
                .A("She")
                .B("eats")
                .C("rice")
                .D("none")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("They play outside.")
                .Type(EnglishType.Verb)
                .Tooltips0("A verb is an action word.")
                .A("They")
                .B("play")
                .C("outside")
                .D("none")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("He reads a book.")
                .Type(EnglishType.Verb)
                .Tooltips0("A verb is an action word.")
                .A("He")
                .B("reads")
                .C("book")
                .D("none")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("We walk home.")
                .Type(EnglishType.Verb)
                .Tooltips0("A verb is an action word.")
                .A("walk")
                .B("home")
                .C("We")
                .D("none")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("The baby cries.")
                .Type(EnglishType.Verb)
                .Tooltips0("A verb is an action word.")
                .A("baby")
                .B("cries")
                .C("the")
                .D("none")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("The dog barks.")
                .Type(EnglishType.Verb)
                .Tooltips0("A verb is an action word.")
                .A("dog")
                .B("barks")
                .C("the")
                .D("none")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("She sings well.")
                .Type(EnglishType.Verb)
                .Tooltips0("A verb is an action word.")
                .A("She")
                .B("sings")
                .C("well")
                .D("none")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("They jump high.")
                .Type(EnglishType.Verb)
                .Tooltips0("A verb is an action word.")
                .A("jump")
                .B("high")
                .C("They")
                .D("none")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("He swims fast.")
                .Type(EnglishType.Verb)
                .Tooltips0("A verb is an action word.")
                .A("He")
                .B("swims")
                .C("fast")
                .D("none")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("She has a red bag.")
                .Type(EnglishType.Adjective)
                .Tooltips0("An adjective describes a noun.")
                .A("bag")
                .B("red")
                .C("has")
                .D("she")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("The tall boy is kind.")
                .Type(EnglishType.Adjective)
                .Tooltips0("An adjective describes a noun.")
                .A("tall")
                .B("boy")
                .C("kind")
                .D("is")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("The small cat is cute.")
                .Type(EnglishType.Adjective)
                .Tooltips0("An adjective describes a noun.")
                .A("small")
                .B("cat")
                .C("cute")
                .D("is")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("The happy girl is singing.")
                .Type(EnglishType.Adjective)
                .Tooltips0("An adjective describes a noun.")
                .A("happy")
                .B("girl")
                .C("singing")
                .D("is")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("He has a big house.")
                .Type(EnglishType.Adjective)
                .Tooltips0("An adjective describes a noun.")
                .A("big")
                .B("house")
                .C("has")
                .D("He")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("She wears a blue dress.")
                .Type(EnglishType.Adjective)
                .Tooltips0("An adjective describes a noun.")
                .A("blue")
                .B("dress")
                .C("wears")
                .D("she")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("The fast car won.")
                .Type(EnglishType.Adjective)
                .Tooltips0("An adjective describes a noun.")
                .A("fast")
                .B("car")
                .C("won")
                .D("the")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("The clean room is nice.")
                .Type(EnglishType.Adjective)
                .Tooltips0("An adjective describes a noun.")
                .A("clean")
                .B("room")
                .C("nice")
                .D("is")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("The smart student passed.")
                .Type(EnglishType.Adjective)
                .Tooltips0("An adjective describes a noun.")
                .A("smart")
                .B("student")
                .C("passed")
                .D("the")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("The beautiful flower blooms.")
                .Type(EnglishType.Adjective)
                .Tooltips0("An adjective describes a noun.")
                .A("beautiful")
                .B("flower")
                .C("blooms")
                .D("the")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("She runs fast.")
                .Type(EnglishType.Verb)
                .Tooltips0("A verb is an action word.")
                .A("She")
                .B("runs")
                .C("fast")
                .D("none")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("The dog is big.")
                .Type(EnglishType.Adjective)
                .Tooltips0("An adjective describes a noun.")
                .A("dog")
                .B("is")
                .C("big")
                .D("the")
                .CorrectQuestion(2)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("They are friends.")
                .Type(EnglishType.Pronoun)
                .Tooltips0("A pronoun replaces a noun")
                .A("friends")
                .B("They")
                .C("are")
                .D("none")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("The teacher is teaching.")
                .Type(EnglishType.Noun)
                .Tooltips0("A noun is the name of a person, place, animal, or thing")
                .A("teacher")
                .B("teaching")
                .C("is")
                .D("the")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("He eats quickly.")
                .Type(EnglishType.Verb)
                .Tooltips0("A verb is an action word.")
                .A("He")
                .B("eats")
                .C("quickly")
                .D("none")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("The red apple is sweet.")
                .Type(EnglishType.Adjective)
                .Tooltips0("An adjective describes a noun.")
                .A("red")
                .B("apple")
                .C("sweet")
                .D("is")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("Maria is dancing.")
                .Type(EnglishType.Noun)
                .Tooltips0("A noun is the name of a person, place, animal, or thing")
                .A("Maria")
                .B("dancing")
                .C("is")
                .D("none")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("She is kind.")
                .Type(EnglishType.Pronoun)
                .Tooltips0("A pronoun replaces a noun")
                .A("She")
                .B("kind")
                .C("is")
                .D("none")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("The boy jumps.")
                .Type(EnglishType.Verb)
                .Tooltips0("A verb is an action word.")
                .A("boy")
                .B("jumps")
                .C("the")
                .D("none")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Easy));

            EnglishSubject.Add(EnglishQuestion.Create("The small dog sleeps.")
                .Type(EnglishType.Adjective)
                .Tooltips0("An adjective describes a noun.")
                .A("small")
                .B("dog")
                .C("sleeps")
                .D("none")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Easy));
            EnglishSubject.Add(EnglishQuestion.Create("Yesterday, I ____ to school.")
                .Type(EnglishType.Tense)
                .Tooltips0("Tense tells when an action happens: past (already happened), present (happening now), or future (will happen).")
                .A("go")
                .B("went")
                .C("going")
                .D("goes")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("She ____ eating now.")
                .Type(EnglishType.Tense)
                .Tooltips0("Tense tells when an action happens: past (already happened), present (happening now), or future (will happen).")
                .A("is")
                .B("was")
                .C("were")
                .D("are")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("They ____ basketball tomorrow.")
                .Type(EnglishType.Tense)
                .Tooltips0("Tense tells when an action happens: past (already happened), present (happening now), or future (will happen).")
                .A("play")
                .B("played")
                .C("will play")
                .D("playing")
                .CorrectQuestion(2)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("He ____ his homework last night.")
                .Type(EnglishType.Tense)
                .Tooltips0("Tense tells when an action happens: past (already happened), present (happening now), or future (will happen).")
                .A("finish")
                .B("finished")
                .C("finishing")
                .D("finishes")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("We ____ lunch now.")
                .Type(EnglishType.Tense)
                .Tooltips0("Tense tells when an action happens: past (already happened), present (happening now), or future (will happen).")
                .A("eat")
                .B("ate")
                .C("eating")
                .D("are eating")
                .CorrectQuestion(3)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("She ____ to the market yesterday.")
                .Type(EnglishType.Tense)
                .Tooltips0("Tense tells when an action happens: past (already happened), present (happening now), or future (will happen).")
                .A("go")
                .B("goes")
                .C("went")
                .D("going")
                .CorrectQuestion(2)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("I ____ my friend tomorrow.")
                .Type(EnglishType.Tense)
                .Tooltips0("Tense tells when an action happens: past (already happened), present (happening now), or future (will happen).")
                .A("see")
                .B("saw")
                .C("will see")
                .D("seeing")
                .CorrectQuestion(2)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("He ____ TV now.")
                .Type(EnglishType.Tense)
                .Tooltips0("Tense tells when an action happens: past (already happened), present (happening now), or future (will happen).")
                .A("watch")
                .B("watches")
                .C("watching")
                .D("is watching")
                .CorrectQuestion(3)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("They ____ basketball yesterday.")
                .Type(EnglishType.Tense)
                .Tooltips0("Tense tells when an action happens: past (already happened), present (happening now), or future (will happen).")
                .A("play")
                .B("played")
                .C("playing")
                .D("plays")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("We ____ dinner later.")
                .Type(EnglishType.Tense)
                .Tooltips0("Tense tells when an action happens: past (already happened), present (happening now), or future (will happen).")
                .A("eat")
                .B("ate")
                .C("will eat")
                .D("eating")
                .CorrectQuestion(2)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("She ____ a letter yesterday.")
                .Type(EnglishType.Tense)
                .Tooltips0("Tense tells when an action happens: past (already happened), present (happening now), or future (will happen).")
                .A("writes")
                .B("wrote")
                .C("writing")
                .D("write")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("I ____ reading a book now.")
                .Type(EnglishType.Tense)
                .Tooltips0("Tense tells when an action happens: past (already happened), present (happening now), or future (will happen).")
                .A("am")
                .B("is")
                .C("was")
                .D("were")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("They ____ to the park tomorrow.")
                .Type(EnglishType.Tense)
                .Tooltips0("Tense tells when an action happens: past (already happened), present (happening now), or future (will happen).")
                .A("goes")
                .B("will go")
                .C("went")
                .D("going")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("He ____ his bike yesterday.")
                .Type(EnglishType.Tense)
                .Tooltips0("Tense tells when an action happens: past (already happened), present (happening now), or future (will happen).")
                .A("rides")
                .B("riding")
                .C("rode")
                .D("ride")
                .CorrectQuestion(2)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("We ____ our homework now.")
                .Type(EnglishType.Tense)
                .Tooltips0("Tense tells when an action happens: past (already happened), present (happening now), or future (will happen).")
                .A("are doing")
                .B("doing")
                .C("did")
                .D("does")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("She ____ the guitar tomorrow.")
                .Type(EnglishType.Tense)
                .Tooltips0("Tense tells when an action happens: past (already happened), present (happening now), or future (will happen).")
                .A("play")
                .B("will play")
                .C("played")
                .D("playing")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("I ____ my room yesterday.")
                .Type(EnglishType.Tense)
                .Tooltips0("Tense tells when an action happens: past (already happened), present (happening now), or future (will happen).")
                .A("cleaned")
                .B("cleaning")
                .C("clean")
                .D("cleans")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("He ____ reading a story now.")
                .Type(EnglishType.Tense)
                .Tooltips0("Tense tells when an action happens: past (already happened), present (happening now), or future (will happen).")
                .A("is")
                .B("am")
                .C("are")
                .D("was")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("They ____ pizza yesterday.")
                .Type(EnglishType.Tense)
                .Tooltips0("Tense tells when an action happens: past (already happened), present (happening now), or future (will happen).")
                .A("ate")
                .B("eat")
                .C("eats")
                .D("eating")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("We ____ to the beach tomorrow.")
                .Type(EnglishType.Tense)
                .Tooltips0("Tense tells when an action happens: past (already happened), present (happening now), or future (will happen).")
                .A("go")
                .B("goes")
                .C("will go")
                .D("going")
                .CorrectQuestion(2)
                .Level(QuestionLevel.Medium));
            EnglishSubject.Add(EnglishQuestion.Create("Which is a correct sentence?")
                .Type(EnglishType.Sentence)
                .Tooltips0("A sentence is a group of words with complete meaning.")
                .A("is she happy")
                .B("She happy is")
                .C("She is happy")
                .D("happy she is")
                .CorrectQuestion(2)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Which is correct?")
                .Type(EnglishType.Sentence)
                .Tooltips0("A sentence is a group of words with complete meaning.")
                .A("He playing now")
                .B("He is playing now")
                .C("He playing is now")
                .D("Playing he now")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Which is correct?")
                .Type(EnglishType.Sentence)
                .Tooltips0("A sentence is a group of words with complete meaning.")
                .A("They are running")
                .B("They running are")
                .C("Running are they")
                .D("Are running they")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Which is correct?")
                .Type(EnglishType.Sentence)
                .Tooltips0("A sentence is a group of words with complete meaning.")
                .A("I am eating")
                .B("Am eating I")
                .C("Eating am I")
                .D("I eating am")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Which is correct?")
                .Type(EnglishType.Sentence)
                .Tooltips0("A sentence is a group of words with complete meaning.")
                .A("She is reading")
                .B("She reading is")
                .C("Reading is she")
                .D("Is reading she")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Which is correct?")
                .Type(EnglishType.Sentence)
                .Tooltips0("A sentence is a group of words with complete meaning.")
                .A("We are playing")
                .B("Playing we are")
                .C("Are we playing")
                .D("We playing are")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Which is correct?")
                .Type(EnglishType.Sentence)
                .Tooltips0("A sentence is a group of words with complete meaning.")
                .A("The dog is barking")
                .B("Dog the is barking")
                .C("Is barking the dog")
                .D("Barking the dog is")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Which is correct?")
                .Type(EnglishType.Sentence)
                .Tooltips0("A sentence is a group of words with complete meaning.")
                .A("The boys are running")
                .B("Are the boys running")
                .C("Boys are the running")
                .D("Running are the boys")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Which is correct?")
                .Type(EnglishType.Sentence)
                .Tooltips0("A sentence is a group of words with complete meaning.")
                .A("She sings a song")
                .B("Sings she a song")
                .C("Song she sings a")
                .D("A song sings she")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Which is correct?")
                .Type(EnglishType.Sentence)
                .Tooltips0("A sentence is a group of words with complete meaning.")
                .A("I like apples")
                .B("Apples like I")
                .C("Like I apples")
                .D("I apples like")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Which is correct?")
                .Type(EnglishType.Sentence)
                .Tooltips0("A sentence is a group of words with complete meaning.")
                .A("They are studying")
                .B("Studying they are")
                .C("Are studying they")
                .D("They studying are")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Which is correct?")
                .Type(EnglishType.Sentence)
                .Tooltips0("A sentence is a group of words with complete meaning.")
                .A("He is swimming")
                .B("Swimming he is")
                .C("Is he swimming")
                .D("He swimming is")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Which is correct?")
                .Type(EnglishType.Sentence)
                .Tooltips0("A sentence is a group of words with complete meaning.")
                .A("The girl is happy")
                .B("Girl the happy is")
                .C("Is happy the girl")
                .D("Happy the girl is")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Which is correct?")
                .Type(EnglishType.Sentence)
                .Tooltips0("A sentence is a group of words with complete meaning.")
                .A("We are learning English")
                .B("English learning we are")
                .C("Are we learning English")
                .D("Learning we English are")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Which is correct?")
                .Type(EnglishType.Sentence)
                .Tooltips0("A sentence is a group of words with complete meaning.")
                .A("I am writing a story")
                .B("Writing I am a story")
                .C("Am writing I a story")
                .D("I writing am a story")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Synonym of HAPPY")
                .Type(EnglishType.Synonym)
                .Tooltips0("A synonym is a word that has the same or similar meaning as another word.")
                .A("Sad")
                .B("Angry")
                .C("Joyful")
                .D("Cry")
                .CorrectQuestion(2)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Synonym of BIG")
                .Type(EnglishType.Synonym)
                .Tooltips0("A synonym is a word that has the same or similar meaning as another word.")
                .A("Small")
                .B("Tiny")
                .C("Huge")
                .D("Little")
                .CorrectQuestion(2)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Synonym of FAST")
                .Type(EnglishType.Synonym)
                .Tooltips0("A synonym is a word that has the same or similar meaning as another word.")
                .A("Slow")
                .B("Quick")
                .C("Late")
                .D("Stop")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Synonym of BEAUTIFUL")
                .Type(EnglishType.Synonym)
                .Tooltips0("A synonym is a word that has the same or similar meaning as another word.")
                .A("Ugly")
                .B("Pretty")
                .C("Bad")
                .D("Dirty")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Synonym of SMART")
                .Type(EnglishType.Synonym)
                .Tooltips0("A synonym is a word that has the same or similar meaning as another word.")
                .A("Intelligent")
                .B("Lazy")
                .C("Weak")
                .D("Slow")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Synonym of SMALL")
                .Type(EnglishType.Synonym)
                .Tooltips0("A synonym is a word that has the same or similar meaning as another word.")
                .A("Tiny")
                .B("Big")
                .C("Huge")
                .D("Large")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Synonym of ANGRY")
                .Type(EnglishType.Synonym)
                .Tooltips0("A synonym is a word that has the same or similar meaning as another word.")
                .A("Mad")
                .B("Happy")
                .C("Calm")
                .D("Kind")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Synonym of BRAVE")
                .Type(EnglishType.Synonym)
                .Tooltips0("A synonym is a word that has the same or similar meaning as another word.")
                .A("Cowardly")
                .B("Bold")
                .C("Scared")
                .D("Weak")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Synonym of STRONG")
                .Type(EnglishType.Synonym)
                .Tooltips0("A synonym is a word that has the same or similar meaning as another word.")
                .A("Weak")
                .B("Powerful")
                .C("Soft")
                .D("Gentle")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Synonym of EASY")
                .Type(EnglishType.Synonym)
                .Tooltips0("A synonym is a word that has the same or similar meaning as another word.")
                .A("Difficult")
                .B("Simple")
                .C("Hard")
                .D("Tough")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Synonym of RICH")
                .Type(EnglishType.Synonym)
                .Tooltips0("A synonym is a word that has the same or similar meaning as another word.")
                .A("Wealthy")
                .B("Poor")
                .C("Empty")
                .D("Weak")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Synonym of HELP")
                .Type(EnglishType.Synonym)
                .Tooltips0("A synonym is a word that has the same or similar meaning as another word.")
                .A("Hurt")
                .B("Assist")
                .C("Stop")
                .D("Block")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Synonym of LAUGH")
                .Type(EnglishType.Synonym)
                .Tooltips0("A synonym is a word that has the same or similar meaning as another word.")
                .A("Cry")
                .B("Giggle")
                .C("Yell")
                .D("Scream")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Synonym of BEGIN")
                .Type(EnglishType.Synonym)
                .Tooltips0("A synonym is a word that has the same or similar meaning as another word.")
                .A("Start")
                .B("End")
                .C("Finish")
                .D("Stop")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Synonym of FINISH")
                .Type(EnglishType.Synonym)
                .Tooltips0("A synonym is a word that has the same or similar meaning as another word.")
                .A("Start")
                .B("End")
                .C("Begin")
                .D("Continue")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Medium));

            EnglishSubject.Add(EnglishQuestion.Create("Who loves reading books?")
                .Type(EnglishType.Comprehension)
                .Tooltips0("Comprehension is understanding what you read. Read the paragraph and answer the questions.")
                .Tooltips1("Anna loves reading books. She goes to the library every Saturday. She reads with her friends.")
                .A("Maria")
                .B("Anna")
                .C("John")
                .D("Teacher")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("Where does Anna go every Saturday?")
                .Type(EnglishType.Comprehension)
                .Tooltips0("Comprehension is understanding what you read. Read the paragraph and answer the questions.")
                .Tooltips1("Anna loves reading books. She goes to the library every Saturday. She reads with her friends.")
                .A("Mall")
                .B("Park")
                .C("Library")
                .D("School")
                .CorrectQuestion(2)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("Who reads with Anna?")
                .Type(EnglishType.Comprehension)
                .Tooltips0("Comprehension is understanding what you read. Read the paragraph and answer the questions.")
                .Tooltips1("Anna loves reading books. She goes to the library every Saturday. She reads with her friends.")
                .A("Her teacher")
                .B("Her friends")
                .C("Her brother")
                .D("Her parents")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("When does Anna go to the library?")
                .Type(EnglishType.Comprehension)
                .Tooltips0("Comprehension is understanding what you read. Read the paragraph and answer the questions.")
                .Tooltips1("Anna loves reading books. She goes to the library every Saturday. She reads with her friends.")
                .A("Sunday")
                .B("Saturday")
                .C("Friday")
                .D("Monday")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("What does Anna do at the library?")
                .Type(EnglishType.Comprehension)
                .Tooltips0("Comprehension is understanding what you read. Read the paragraph and answer the questions.")
                .Tooltips1("Anna loves reading books. She goes to the library every Saturday. She reads with her friends.")
                .A("Plays games")
                .B("Reads books")
                .C("Watches TV")
                .D("Sleeps")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("What kind of animal is described?")
                .Type(EnglishType.Comprehension)
                .Tooltips0("Comprehension is understanding what you read. Read the paragraph and answer the questions.")
                .Tooltips1("The dog is very smart. It can follow commands and help its owner. It loves to play outside.")
                .A("Cat")
                .B("Dog")
                .C("Bird")
                .D("Rabbit")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("What can the dog do?")
                .Type(EnglishType.Comprehension)
                .Tooltips0("Comprehension is understanding what you read. Read the paragraph and answer the questions.")
                .Tooltips1("The dog is very smart. It can follow commands and help its owner. It loves to play outside.")
                .A("Sleep all day")
                .B("Follow commands")
                .C("Eat quietly")
                .D("Run only")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("Who does the dog help?")
                .Type(EnglishType.Comprehension)
                .Tooltips0("Comprehension is understanding what you read. Read the paragraph and answer the questions.")
                .Tooltips1("The dog is very smart. It can follow commands and help its owner. It loves to play outside.")
                .A("Its friend")
                .B("Its owner")
                .C("Its neighbor")
                .D("Its teacher")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("Where does the dog like to play?")
                .Type(EnglishType.Comprehension)
                .Tooltips0("Comprehension is understanding what you read. Read the paragraph and answer the questions.")
                .Tooltips1("The dog is very smart. It can follow commands and help its owner. It loves to play outside.")
                .A("Inside the house")
                .B("Outside")
                .C("In a cage")
                .D("At school")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("How is the dog described?")
                .Type(EnglishType.Comprehension)
                .Tooltips0("Comprehension is understanding what you read. Read the paragraph and answer the questions.")
                .Tooltips1("The dog is very smart. It can follow commands and help its owner. It loves to play outside.")
                .A("Lazy")
                .B("Smart")
                .C("Weak")
                .D("Funny")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("Who likes gardening?")
                .Type(EnglishType.Comprehension)
                .Tooltips0("Comprehension is understanding what you read. Read the paragraph and answer the questions.")
                .Tooltips1("Peter and his sister like to garden. They plant flowers and vegetables. Every morning, they water the plants.")
                .A("Peter alone")
                .B("Peter and his sister")
                .C("His friends")
                .D("Only his sister")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("What do they plant?")
                .Type(EnglishType.Comprehension)
                .Tooltips0("Comprehension is understanding what you read. Read the paragraph and answer the questions.")
                .Tooltips1("Peter and his sister like to garden. They plant flowers and vegetables. Every morning, they water the plants.")
                .A("Flowers and vegetables")
                .B("Cars and bikes")
                .C("Clothes")
                .D("Books")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("How often do they water the plants?")
                .Type(EnglishType.Comprehension)
                .Tooltips0("Comprehension is understanding what you read. Read the paragraph and answer the questions.")
                .Tooltips1("Peter and his sister like to garden. They plant flowers and vegetables. Every morning, they water the plants.")
                .A("Every evening")
                .B("Every morning")
                .C("Once a week")
                .D("Once a month")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("What is the main idea of the paragraph?")
                .Type(EnglishType.Comprehension)
                .Tooltips0("Comprehension is understanding what you read. Read the paragraph and answer the questions.")
                .Tooltips1("Peter and his sister like to garden. They plant flowers and vegetables. Every morning, they water the plants.")
                .A("Gardening is fun")
                .B("Peter plays outside")
                .C("Peter and his sister grow plants")
                .D("They love animals")
                .CorrectQuestion(2)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("What is Peter’s hobby?")
                .Type(EnglishType.Comprehension)
                .Tooltips0("Comprehension is understanding what you read. Read the paragraph and answer the questions.")
                .Tooltips1("Peter and his sister like to garden. They plant flowers and vegetables. Every morning, they water the plants.")
                .A("Reading")
                .B("Gardening")
                .C("Swimming")
                .D("Dancing")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("Who loves to draw?")
                .Type(EnglishType.Comprehension)
                .Tooltips0("Comprehension is understanding what you read. Read the paragraph and answer the questions.")
                .Tooltips1("Lucy loves to draw. She draws animals, houses, and trees. She shares her drawings with her friends at school.")
                .A("Lily")
                .B("Lucy")
                .C("Lisa")
                .D("Leah")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("What does Lucy draw?")
                .Type(EnglishType.Comprehension)
                .Tooltips0("Comprehension is understanding what you read. Read the paragraph and answer the questions.")
                .Tooltips1("Lucy loves to draw. She draws animals, houses, and trees. She shares her drawings with her friends at school.")
                .A("Animals, houses, and trees")
                .B("Cars and planes")
                .C("Clothes")
                .D("Books")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("Who does Lucy share her drawings with?")
                .Type(EnglishType.Comprehension)
                .Tooltips0("Comprehension is understanding what you read. Read the paragraph and answer the questions.")
                .Tooltips1("Lucy loves to draw. She draws animals, houses, and trees. She shares her drawings with her friends at school.")
                .A("Her teacher")
                .B("Her parents")
                .C("Her friends")
                .D("Nobody")
                .CorrectQuestion(2)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("Where does Lucy share her drawings?")
                .Type(EnglishType.Comprehension)
                .Tooltips0("Comprehension is understanding what you read. Read the paragraph and answer the questions.")
                .Tooltips1("Lucy loves to draw. She draws animals, houses, and trees. She shares her drawings with her friends at school.")
                .A("At home")
                .B("At school")
                .C("At the park")
                .D("At the library")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("What is Lucy’s hobby?")
                .Type(EnglishType.Comprehension)
                .Tooltips0("Comprehension is understanding what you read. Read the paragraph and answer the questions.")
                .Tooltips1("Lucy loves to draw. She draws animals, houses, and trees. She shares her drawings with her friends at school.")
                .A("Singing")
                .B("Drawing")
                .C("Reading")
                .D("Dancing")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("He ____ very tall.")
                .Type(EnglishType.Grammar)
                .Tooltips0("Grammar is the correct way to use words and sentences.")
                .A("is")
                .B("are")
                .C("am")
                .D("be")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("They ____ playing outside now.")
                .Type(EnglishType.Grammar)
                .Tooltips0("Grammar is the correct way to use words and sentences.")
                .A("is")
                .B("are")
                .C("am")
                .D("be")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("I ____ my homework yesterday.")
                .Type(EnglishType.Grammar)
                .Tooltips0("Grammar is the correct way to use words and sentences.")
                .A("do")
                .B("did")
                .C("does")
                .D("doing")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("She ____ reading a book now.")
                .Type(EnglishType.Grammar)
                .Tooltips0("Grammar is the correct way to use words and sentences.")
                .A("is")
                .B("are")
                .C("am")
                .D("be")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("We ____ going to the park tomorrow.")
                .Type(EnglishType.Grammar)
                .Tooltips0("Grammar is the correct way to use words and sentences.")
                .A("is")
                .B("are")
                .C("am")
                .D("be")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("The cat ____ sleeping on the sofa.")
                .Type(EnglishType.Grammar)
                .Tooltips0("Grammar is the correct way to use words and sentences.")
                .A("is")
                .B("are")
                .C("am")
                .D("be")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("They ____ happy yesterday.")
                .Type(EnglishType.Grammar)
                .Tooltips0("Grammar is the correct way to use words and sentences.")
                .A("is")
                .B("was")
                .C("were")
                .D("be")
                .CorrectQuestion(2)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("He ____ a student.")
                .Type(EnglishType.Grammar)
                .Tooltips0("Grammar is the correct way to use words and sentences.")
                .A("is")
                .B("are")
                .C("am")
                .D("be")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("I ____ like apples.")
                .Type(EnglishType.Grammar)
                .Tooltips0("Grammar is the correct way to use words and sentences.")
                .A("do")
                .B("does")
                .C("did")
                .D("doing")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("She ____ singing beautifully.")
                .Type(EnglishType.Grammar)
                .A("is")
                .B("are")
                .C("am")
                .D("be")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("We ____ watched a movie last night.")
                .Type(EnglishType.Grammar)
                .Tooltips0("Grammar is the correct way to use words and sentences.")
                .A("have")
                .B("has")
                .C("had")
                .D("having")
                .CorrectQuestion(2)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("They ____ eat lunch now.")
                .Type(EnglishType.Grammar)
                .Tooltips0("Grammar is the correct way to use words and sentences.")
                .A("is")
                .B("are")
                .C("am")
                .D("be")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("He ____ writing a story yesterday.")
                .Type(EnglishType.Grammar)
                .Tooltips0("Grammar is the correct way to use words and sentences.")
                .A("was")
                .B("were")
                .C("is")
                .D("are")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("I ____ going to school every day.")
                .Type(EnglishType.Grammar)
                .Tooltips0("Grammar is the correct way to use words and sentences.")
                .A("am")
                .B("is")
                .C("are")
                .D("be")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("The birds ____ flying in the sky now.")
                .Type(EnglishType.Grammar)
                .Tooltips0("Grammar is the correct way to use words and sentences.")
                .A("is")
                .B("are")
                .C("am")
                .D("be")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("She ____ read a book yesterday.")
                .Type(EnglishType.Grammar)
                .Tooltips0("Grammar is the correct way to use words and sentences.")
                .A("did")
                .B("do")
                .C("does")
                .D("doing")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("They ____ cleaning their room now.")
                .Type(EnglishType.Grammar)
                .Tooltips0("Grammar is the correct way to use words and sentences.")
                .A("is")
                .B("are")
                .C("am")
                .D("be")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("I ____ very tired yesterday.")
                .Type(EnglishType.Grammar)
                .Tooltips0("Grammar is the correct way to use words and sentences.")
                .A("am")
                .B("was")
                .C("is")
                .D("are")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("He ____ plays basketball every day.")
                .Type(EnglishType.Grammar)
                .Tooltips0("Grammar is the correct way to use words and sentences.")
                .A("do")
                .B("does")
                .C("did")
                .D("doing")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("We ____ visited the zoo last week.")
                .Type(EnglishType.Grammar)
                .Tooltips0("Grammar is the correct way to use words and sentences.")
                .A("has")
                .B("have")
                .C("had")
                .D("having")
                .CorrectQuestion(2)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("What is Sam’s hobby?")
                .Type(EnglishType.Paragraph)
                .Tooltips0("Sam enjoys playing football. Every afternoon, he practices with his team. He also watches football matches on TV. Sam hopes to become a professional football player one day.")
                .A("Swimming")
                .B("Football")
                .C("Basketball")
                .D("Reading")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("When does Sam practice?")
                .Type(EnglishType.Paragraph)
                .Tooltips0("Sam enjoys playing football. Every afternoon, he practices with his team. He also watches football matches on TV. Sam hopes to become a professional football player one day.")
                .A("Morning")
                .B("Afternoon")
                .C("Evening")
                .D("Night")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("Who does Sam practice with?")
                .Type(EnglishType.Paragraph)
                .Tooltips0("Sam enjoys playing football. Every afternoon, he practices with his team. He also watches football matches on TV. Sam hopes to become a professional football player one day.")
                .A("His friends")
                .B("His family")
                .C("His team")
                .D("Alone")
                .CorrectQuestion(2)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("What does Sam watch on TV?")
                .Type(EnglishType.Paragraph)
                .Tooltips0("Sam enjoys playing football. Every afternoon, he practices with his team. He also watches football matches on TV. Sam hopes to become a professional football player one day.")
                .A("Cooking shows")
                .B("Football matches")
                .C("Cartoons")
                .D("News")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("What is Sam’s dream?")
                .Type(EnglishType.Paragraph)
                .Tooltips0("Sam enjoys playing football. Every afternoon, he practices with his team. He also watches football matches on TV. Sam hopes to become a professional football player one day.")
                .A("To be a teacher")
                .B("To be a football player")
                .C("To be a doctor")
                .D("To be a singer")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("How is the sun today?")
                .Type(EnglishType.Paragraph)
                .Tooltips0("The sun is bright today. Children are playing outside. Birds are singing in the trees. Everyone is enjoying the sunny day.")
                .A("Bright")
                .B("Dark")
                .C("Hot")
                .D("Cold")
                .CorrectQuestion(0)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("Who is playing outside?")
                .Type(EnglishType.Paragraph)
                .Tooltips0("The sun is bright today. Children are playing outside. Birds are singing in the trees. Everyone is enjoying the sunny day.")
                .A("Adults")
                .B("Children")
                .C("Animals")
                .D("Teachers")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("What are the birds doing?")
                .Type(EnglishType.Paragraph)
                .Tooltips0("The sun is bright today. Children are playing outside. Birds are singing in the trees. Everyone is enjoying the sunny day.")
                .A("Sleeping")
                .B("Singing")
                .C("Eating")
                .D("Flying")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("What kind of day is it?")
                .Type(EnglishType.Paragraph)
                .Tooltips0("The sun is bright today. Children are playing outside. Birds are singing in the trees. Everyone is enjoying the sunny day.")
                .A("Rainy")
                .B("Sunny")
                .C("Cloudy")
                .D("Snowy")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));

            EnglishSubject.Add(EnglishQuestion.Create("What kind of day is it?")
                .Type(EnglishType.Paragraph)
                .Tooltips0("The sun is bright today. Children are playing outside. Birds are singing in the trees. Everyone is enjoying the sunny day.")
                .A("Children")
                .B("Everyone")
                .C("Teachers")
                .D("Birds")
                .CorrectQuestion(1)
                .Level(QuestionLevel.Hard));
        }
    }
}
