using System.Collections.Generic;

[System.Serializable]
public class Question
{
    public string questionText;
    public string[] answers;       // 4 خيارات
    public int correctIndex;       // رقم الإجابة الصحيحة (0-3)
    public string funFact;         // حقيقة مدهشة تظهر بعد الإجابة
    public string emoji;           // رمز مرئي للسؤال
}

public static class QuizData
{
    public static Dictionary<string, List<Question>> AllQuestions =
        new Dictionary<string, List<Question>>
    {
        // =================== عالم الفضاء ===================
        { "Space", new List<Question>
            {
                new Question {
                    questionText = "كم عدد الكواكب في مجموعتنا الشمسية؟",
                    answers = new[]{ "6", "7", "8", "9" },
                    correctIndex = 2,
                    funFact = "المجموعة الشمسية تحتوي على 8 كواكب! بلوتو أصبح قزماً منذ 2006 🪐",
                    emoji = "🪐"
                },
                new Question {
                    questionText = "أي كوكب هو الأكبر في مجموعتنا الشمسية؟",
                    answers = new[]{ "زحل", "المشتري", "الأرض", "أورانوس" },
                    correctIndex = 1,
                    funFact = "المشتري ضخم جداً لدرجة أن 1300 كرة أرضية تتسع داخله! 🌟",
                    emoji = "🔭"
                },
                new Question {
                    questionText = "ماذا نسمي الشخص الذي يسافر إلى الفضاء؟",
                    answers = new[]{ "طيار", "رائد فضاء", "ملاح", "مستكشف" },
                    correctIndex = 1,
                    funFact = "رواد الفضاء يرون شروق الشمس 16 مرة في اليوم الواحد! 🚀",
                    emoji = "👨‍🚀"
                },
                new Question {
                    questionText = "لماذا يكون الليل والنهار؟",
                    answers = new[]{ "الشمس تطفأ", "الأرض تدور حول نفسها", "القمر يحجب الشمس", "الغيوم تغطي الشمس" },
                    correctIndex = 1,
                    funFact = "الأرض تدور دورة كاملة كل 24 ساعة، لهذا نرى الشمس من ناحية واحدة فقط! 🌍",
                    emoji = "🌅"
                },
                new Question {
                    questionText = "ما لون القمر في الحقيقة؟",
                    answers = new[]{ "أبيض", "رمادي", "أصفر", "فضي" },
                    correctIndex = 1,
                    funFact = "القمر في الحقيقة رمادي داكن، لكنه يبدو أبيض لأنه يعكس ضوء الشمس! 🌙",
                    emoji = "🌙"
                },
            }
        },

        // =================== عالم البحر ===================
        { "Ocean", new List<Question>
            {
                new Question {
                    questionText = "كيف يتنفس السمك تحت الماء؟",
                    answers = new[]{ "بأنفه", "بخياشيمه", "يحبس نفسه", "بجلده" },
                    correctIndex = 1,
                    funFact = "الخياشيم تصفي الأكسجين من الماء تماماً كما تصفي رئتك الأكسجين من الهواء! 🐠",
                    emoji = "🐟"
                },
                new Question {
                    questionText = "ما أكبر حيوان في البحر؟",
                    answers = new[]{ "القرش الأبيض", "الحوت الأزرق", "الأخطبوط", "سمكة القرش الحوت" },
                    correctIndex = 1,
                    funFact = "الحوت الأزرق أكبر من أي ديناصور عاش على الأرض! قلبه بحجم سيارة صغيرة 🐋",
                    emoji = "🐳"
                },
                new Question {
                    questionText = "ماذا يأكل نجم البحر؟",
                    answers = new[]{ "العشب البحري", "الأسماك الكبيرة", "المحار والأصداف", "الخبز" },
                    correctIndex = 2,
                    funFact = "نجم البحر يخرج معدته من جسمه ليهضم طعامه خارجياً! يا له من غريب! ⭐",
                    emoji = "⭐"
                },
                new Question {
                    questionText = "كم عدد أذرع الأخطبوط؟",
                    answers = new[]{ "6", "8", "10", "12" },
                    correctIndex = 1,
                    funFact = "الأخطبوط له 3 قلوب ودمه لون أزرق! وكل ذراع لها دماغ صغير خاص! 🐙",
                    emoji = "🐙"
                },
                new Question {
                    questionText = "ما هو الحيوان البحري الأسرع؟",
                    answers = new[]{ "الدلفين", "القرش", "سمكة أبو شراع", "سلحفاة البحر" },
                    correctIndex = 2,
                    funFact = "سمكة أبو شراع تسبح بسرعة 110 كم في الساعة — أسرع من سيارة في المدينة! 🏄",
                    emoji = "🌊"
                },
            }
        },

        // =================== عالم الغابة ===================
        { "Forest", new List<Question>
            {
                new Question {
                    questionText = "أي حيوان يشتهر بحفظ الجوز لفصل الشتاء؟",
                    answers = new[]{ "الأرنب", "الدب", "السنجاب", "الثعلب" },
                    correctIndex = 2,
                    funFact = "السنجاب يخبئ آلاف الجوزات ولكنه ينسى مكان 25% منها — فتنبت أشجاراً جديدة! 🌳",
                    emoji = "🐿️"
                },
                new Question {
                    questionText = "لماذا تتغير ألوان أوراق الشجر في الخريف؟",
                    answers = new[]{ "البرد يصبغها", "الشجرة تتوقف عن صنع الكلوروفيل", "الريح تغسلها", "الحشرات تأكلها" },
                    correctIndex = 1,
                    funFact = "الكلوروفيل (اللون الأخضر) يختبئ كل الخريف، فتظهر الألوان الأصفر والبرتقالي التي كانت موجودة دائماً! 🍂",
                    emoji = "🍁"
                },
                new Question {
                    questionText = "ما الذي يجعل الضفدع يقفز عالياً جداً؟",
                    answers = new[]{ "أجنحة خفية", "ساقاه الخلفيتان القويتان", "ذيله", "رياح خاصة" },
                    correctIndex = 1,
                    funFact = "الضفدع يستطيع القفز 20 ضعف طول جسمه! لو فعل الإنسان نفس الشيء لقفز 30 متراً! 🐸",
                    emoji = "🐸"
                },
                new Question {
                    questionText = "أي طائر لا يستطيع الطيران؟",
                    answers = new[]{ "البطريق", "الببغاء", "العصفور", "الحمام" },
                    correctIndex = 0,
                    funFact = "البطريق لا يطير لكنه يسبح بسرعة 36 كم/س تحت الماء — إنه يطير في الماء! 🐧",
                    emoji = "🐧"
                },
                new Question {
                    questionText = "ماذا يأكل الدب في الغالب؟",
                    answers = new[]{ "اللحم فقط", "الأسماك فقط", "كل شيء: نباتات وأسماك وعسل", "الحشرات فقط" },
                    correctIndex = 2,
                    funFact = "الدب يأكل حتى 20,000 سعرة حرارية في اليوم قبل الشتاء — 20 ضعف ما يأكله الإنسان! 🐻",
                    emoji = "🐻"
                },
            }
        },

        // =================== عالم جسم الإنسان ===================
        { "Body", new List<Question>
            {
                new Question {
                    questionText = "كم مرة يضرب قلبك في اليوم الواحد؟",
                    answers = new[]{ "1000 مرة", "10,000 مرة", "100,000 مرة", "1,000,000 مرة" },
                    correctIndex = 2,
                    funFact = "قلبك ينبض 100 ألف مرة يومياً ولا يتعب أبداً! أقوى مضخة في الكون 💪",
                    emoji = "❤️"
                },
                new Question {
                    questionText = "لماذا نتثاءب؟",
                    answers = new[]{ "لأننا ملّيم", "لإدخال المزيد من الأكسجين للدماغ", "لأن الفم يريد أن يتمدد", "لأننا مرضى" },
                    correctIndex = 1,
                    funFact = "التثاؤب معدٍ حتى بين الكلاب! وحتى الأجنة في بطن أمهم يتثاءبون! 😮",
                    emoji = "😮"
                },
                new Question {
                    questionText = "كم عظمة في جسم الإنسان البالغ؟",
                    answers = new[]{ "106", "206", "306", "406" },
                    correctIndex = 1,
                    funFact = "تولد وعندك 300 عظمة! لكنها تندمج مع الوقت حتى تصير 206 عند الكبر! 🦴",
                    emoji = "🦴"
                },
                new Question {
                    questionText = "ما وظيفة الكلى؟",
                    answers = new[]{ "تضخ الدم", "تنقي الدم وتصنع البول", "تهضم الطعام", "تحارب الجراثيم" },
                    correctIndex = 1,
                    funFact = "كلياك تنقي كل دمك 40 مرة في اليوم — كأنهما مصنع تنقية لا يتوقف! 🔬",
                    emoji = "🫁"
                },
                new Question {
                    questionText = "لماذا نحتاج النوم؟",
                    answers = new[]{ "لأن الجسم يتعب فقط", "الدماغ ينظف نفسه ويحفظ ذكرياتك", "لأن القلب يريد الراحة", "لأن العيون تتعب" },
                    correctIndex = 1,
                    funFact = "أثناء نومك دماغك يحذف الأفكار الغير مهمة ويحفظ ما تعلمته — النوم أقوى درس! 😴",
                    emoji = "😴"
                },
            }
        },
    };
}
