# 🎲 מערכת הגרלת מספרים

מערכת הגרלת מספרים מתקדמת עם ממשק משתמש בעברית ו-API מקצועי.

![Main Screen](docs/screenshots/main-screen.png)

## ✨ תכונות

- **הגרלת מספר יחיד** - הגרלה פשוטה של מספר אחד בטווח נתון
- **הגרלת מספרים מרובים** - הגרלת מספר מספרים עם אפשרות למספרים ייחודיים
- **הגרלה מותאמת אישית** - הגדרות מתקדמות לפי הצרכים
- **היסטוריית הגרלות** - מעקב אחר ההגרלות הקודמות
- **ממשק בעברית** - תמיכה מלאה בעברית מימין לשמאל
- **עיצוב רספונסיבי** - מתאים למחשב ומובייל

## 🛠️ טכנולוגיות

### Backend
- **.NET Core 8** - Web API
- **C#** - שפת התכנות
- **Swagger** - תיעוד API
- **CORS** - תמיכה בקריאות מדומיינים שונים
- **Dependency Injection** - ארכיטקטורה מודולרית

### Frontend
- **Vue.js 3** - מסגרת JavaScript
- **Vanilla JavaScript** - ללא תלויות נוספות
- **CSS3** - עיצוב מתקדם עם Flexbox/Grid
- **Axios** - קריאות HTTP
- **Responsive Design** - תמיכה במכשירים שונים

## 🚀 איך להתחיל

### דרישות מקדימות
- .NET 8 SDK
- דפדפן מודרני (Chrome, Firefox, Safari, Edge)

### הפעלת הפרויקט

#### Backend
```bash
cd backend/LotteryApi
dotnet restore
dotnet run
```
השרת יעלה על: `http://localhost:5212`

#### Frontend

**אפשרות 1: פתיחה ישירה**
```bash
cd frontend
# פתח את index.html בדפדפן
```

**אפשרות 2: עם Live Server (מומלץ)**
```bash
cd frontend
# פתח VS Code והשתמש ב-Live Server extension
```

**אפשרות 3: עם שרת HTTP פשוט**
```bash
cd frontend
python -m http.server 8080
# או
npx serve .
```

## 📡 API Endpoints

### הגרלת מספר יחיד
```http
GET /api/lottery/single?min=1&max=100
```

### הגרלת מספרים מרובים
```http
GET /api/lottery/multiple?count=6&min=1&max=37&unique=true
```

### הגרלה מותאמת
```http
POST /api/lottery/custom
Content-Type: application/json

{
  "count": 7,
  "min": 1,
  "max": 37,
  "unique": true
}
```

### בדיקת בריאות
```http
GET /api/lottery/health
```

## 🏗️ ארכיטקטורה

### Backend Structure
```
backend/LotteryApi/
├── Controllers/         # API Controllers
├── Services/           # Business Logic
├── Models/            # Data Models
├── Configuration/     # App Configuration
└── Properties/        # Launch Settings
```

### Frontend Structure
```
frontend/
├── src/
│   ├── components/    # Vue Components
│   ├── services/     # API Services
│   ├── utils/        # Helper Functions
│   └── styles/       # CSS Styles
└── index.html        # Main HTML File
```

## 🎯 דוגמאות שימוש

### הגרלת לוטו (6/37)
- כמות: 6
- טווח: 1-37
- ייחודיים: כן

### הגרלת קוביה
- כמות: 1
- טווח: 1-6

### הגרלת בינגו
- כמות: 25
- טווח: 1-75
- ייחודיים: כן

## 🔧 פיתוח

### הוספת תכונות חדשות
1. הוסף endpoint חדש ב-`LotteryController`
2. עדכן את `ILotteryService` ו-`Lott