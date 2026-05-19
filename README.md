# 🌍 كاشف الكون — Explorer Quest

لعبة تعليمية مغامرة للأطفال عمر 5-8 سنوات. تستكشف 4 عوالم مدهشة وتتعلم حقائق علمية مذهلة!

---

## 🎮 محتوى اللعبة

| العالم | الموضوع | عدد الأسئلة |
|--------|---------|-------------|
| 🚀 الفضاء | كواكب، نجوم، رواد فضاء | 5 |
| 🐠 البحر | أسماك، حيوانات بحرية | 5 |
| 🌿 الغابة | حيوانات، أشجار | 5 |
| ❤️ جسم الإنسان | قلب، دماغ، عظام | 5 |

---

## 🛠️ خطوات البناء من الصفر

### الخطوة 1 — تثبيت الأدوات

1. **Unity Hub**: https://unity.com/download  
   - اختر Unity **6000.x LTS** (مجاني)
   - عند التثبيت، أضف **Android Build Support** + **Android SDK & NDK Tools**

2. **Git**: https://git-scm.com/downloads

3. **حساب GitHub**: https://github.com (مجاني)

---

### الخطوة 2 — إنشاء مشروع Unity

```
1. افتح Unity Hub
2. اضغط New Project
3. اختر قالب: 2D (Core)
4. اسم المشروع: ExplorerQuestUnity
5. اضغط Create Project
```

---

### الخطوة 3 — نسخ الملفات

```
1. انسخ مجلد Assets/Scripts كاملاً إلى مشروع Unity
2. داخل Unity، في Project window:
   - أنشئ مجلدات: Scenes / Sprites / Audio / Resources
```

---

### الخطوة 4 — بناء المشاهد

#### مشهد MainMenu
```
File > New Scene > Save as "MainMenu"

أضف هذه UI Elements:
- Canvas (Screen Space - Overlay, Scale: Scale With Screen Size, 1080x1920)
  ├── Text (عنوان): "كاشف الكون 🌍"
  ├── Text (نجوم): id="starsText"
  ├── Panel (شبكة العوالم)
  │   ├── Button_Space  → WorldButton(0) ← text: "🚀 الفضاء"
  │   ├── Button_Ocean  → WorldButton(1) ← text: "🐠 البحر"
  │   ├── Button_Forest → WorldButton(2) ← text: "🌿 الغابة"
  │   └── Button_Body   → WorldButton(3) ← text: "❤️ جسم الإنسان"
  └── GameObject فارغ + MainMenuController script
```

**ربط السكريبت:**
- اسحب `MainMenuController.cs` على GameObject فارغ
- اربط starsText، worldButtons، lockIcons، costTexts في Inspector
- كل زر: `OnClick()` → `MainMenuController.OnWorldButtonClicked(رقم_العالم)`

#### مشهد GameScene
```
File > New Scene > Save as "GameScene"

Canvas
  ├── Text (questionText) - وسط الشاشة، خط كبير
  ├── Text (emojiText)    - رمز السؤال
  ├── Text (progressText) - أعلى يمين "1/5"
  ├── Text (scoreText)    - أعلى يسار "⭐ 0"
  ├── Panel (أزرار الإجابات) - شبكة 2×2
  │   ├── Button_A (answerButtons[0])
  │   ├── Button_B (answerButtons[1])
  │   ├── Button_C (answerButtons[2])
  │   └── Button_D (answerButtons[3])
  ├── Panel (funFactPanel) - مخفي افتراضياً
  │   ├── Text (funFactText)
  │   └── Button "التالي" → OnNextQuestion()
  ├── Panel (celebrationPanel) - مخفي افتراضياً
  │   └── Text (celebrationText)
  └── Panel (gameOverPanel) - مخفي افتراضياً
      ├── Text (finalScoreText)
      └── Button "القائمة" → ReturnToMenu()

GameObject: QuizController + سكريبت QuizController.cs
```

**إضافة الـ Scenes لـ Build:**
```
File > Build Settings > Add Open Scenes
أضف: MainMenu ثم GameScene
تأكد الترتيب: MainMenu=0, GameScene=1
```

---

### الخطوة 5 — ضبط إعدادات Android

```
Edit > Project Settings > Player

Android tab:
- Company Name: اسمك
- Product Name: ExplorerQuest
- Package Name: com.yourname.explorerquest
- Minimum API Level: Android 7.0 (API 24)
- Target API Level: Automatic (highest installed)
- Scripting Backend: IL2CPP
- Target Architecture: ARM64

Other Settings:
- Color Space: Linear
- Graphics API: OpenGLES3
```

---

### الخطوة 6 — رفع على GitHub

```bash
# في مجلد المشروع
git init
git add .
git commit -m "Initial commit: Explorer Quest game"

# أنشئ Repo على github.com ثم:
git remote add origin https://github.com/USERNAME/ExplorerQuest.git
git branch -M main
git push -u origin main
```

---

### الخطوة 7 — ضبط GitHub Actions للبناء التلقائي

#### الحصول على Unity License (مجاني):
```
1. سجّل دخول على: https://license.unity3d.com
2. احصل على Personal License (مجاني)
3. احفظ محتوى ملف .ulf
```

#### إضافة Secrets إلى GitHub:
```
GitHub Repo > Settings > Secrets and variables > Actions > New secret

أضف هذه الـ Secrets:
UNITY_LICENSE  → (محتوى ملف .ulf كاملاً)
UNITY_EMAIL    → (إيميل حساب Unity)
UNITY_PASSWORD → (كلمة مرور Unity)
```

#### تشغيل البناء:
```
Actions > Build Android APK > Run workflow
⏳ انتظر 15-20 دقيقة
✅ حمّل الـ APK من: Actions > آخر workflow > Artifacts
```

---

### الخطوة 8 — تثبيت APK على الجوال

```
1. على الأندرويد: الإعدادات > الأمان > السماح بمصادر غير معروفة ✓
2. انقل ملف APK للجوال (واتساب / USB / Drive)
3. اضغط على الملف للتثبيت
4. العب! 🎉
```

---

## 📁 هيكل المشروع

```
ExplorerQuest/
├── .github/
│   └── workflows/
│       └── build-android.yml    ← بناء APK تلقائي
├── ExplorerQuestUnity/
│   ├── Assets/
│   │   ├── Scripts/
│   │   │   ├── GameManager.cs       ← إدارة اللعبة والنجوم
│   │   │   ├── QuizData.cs          ← كل الأسئلة (20 سؤال)
│   │   │   ├── MainMenuController.cs ← شاشة الرئيسية
│   │   │   ├── QuizController.cs    ← منطق اللعب
│   │   │   ├── SoundManager.cs      ← الأصوات
│   │   │   └── StarAnimation.cs     ← تأثير النجوم
│   │   ├── Scenes/
│   │   │   ├── MainMenu.unity
│   │   │   └── GameScene.unity
│   │   ├── Sprites/                 ← صور مجانية من Kenney.nl
│   │   └── Audio/                   ← أصوات من FreeSound.org
│   └── ProjectSettings/
├── .gitignore
└── README.md
```

---

## 🎨 مصادر مجانية للرسومات والأصوات

| المصدر | ما تجد فيه | الرابط |
|--------|-----------|--------|
| Kenney.nl | رسومات عالية الجودة | https://kenney.nl/assets |
| FreeSound.org | أصوات وموسيقى | https://freesound.org |
| OpenGameArt | كل شيء | https://opengameart.org |
| Google Fonts | خطوط عربية | https://fonts.google.com |

---

## 🚀 إضافة المزيد لاحقاً

- [ ] عالم التاريخ (الفراعنة، الحضارات)
- [ ] وضع المنافسة بين طفلين
- [ ] دعم اللغة الإنجليزية
- [ ] شاشة إنجازات وميداليات
- [ ] نشر على Google Play (مجاني)

---

## 📄 الترخيص

MIT License — حر الاستخدام والتعديل والتوزيع.
