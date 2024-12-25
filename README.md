# Html Serializer

## תיאור הפרויקט

פרויקט Html Serializer מיועד ליצירת כלי לעיבוד HTML. כלי זה יכול לשמש כבסיס ליצירת **זחלן רשת** (Web Crawler).

**זחלן רשת** הוא מנגנון שקורא אתרי אינטרנט, מנתח את ה-HTML שלהם, ומחלץ מידע רלוונטי. כך, לדוגמה, פועלים מנועי חיפוש כמו Google, אשר סורקים את האינטרנט, מאנדקסים מידע ומאפשרים חיפוש מבוסס מילות מפתח.

### שימושים אפשריים בזחילה:
1. ניתוח אתרים כדי לזהות טכנולוגיות וספריות שהם משתמשים בהן.
2. חילוץ נתונים מאתרי מסחר אלקטרוני או אתרי יד שנייה להצגה באתר אחר.
3. שימושים נוספים רבים.

בפרויקט זה נפתח קוד בסיסי המאפשר לנו בהמשך לבנות זחלן משלנו. הכלי יכלול שני רכיבים עיקריים:

1. **Html Serializer**
2. **Html Query**

---

## Html Serializer

**סיריאליזציה** היא תהליך של המרת מידע מפורמט מסוים לאובייקטים בשפת תכנות. Html Serializer יקרא דף HTML, יפרק את התוכן שלו, וימיר את ה-HTML לאובייקטים של C#.

### שלבי הפיתוח

#### 1. טעינת דף HTML
השתמשו באובייקט `HttpClient` כדי לקרוא דף HTML. לדוגמה:

```csharp
public async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}
```

#### 2. פירוק HTML לתגים
- השתמשו בביטויים רגולריים (Regular Expressions) לזיהוי תגים ב-HTML.
- נקו תווים ריקים, מעברי שורה, ורווחים מיותרים.

#### 3. מחלקת HtmlElement
צרו מחלקה המייצגת תג HTML עם המאפיינים הבאים:
- **Id**
- **Name**
- **Attributes** (רשימה)
- **Classes** (רשימה)
- **InnerHtml**
- יחסים:
  - **Parent**
  - **Children** (לבניית עץ אובייקטים).

#### 4. מחלקת HtmlHelper
פיתוח מחלקת עזר להעמסה נוחה של רשימת תגים ב-HTML:
- טעינת נתונים מקבצי JSON (תגים כלליים ותגים סגורים עצמאית).
- שימוש ב-`JsonSerializer` לפירוק הנתונים למערכי מחרוזות.

#### 5. Singleton Design Pattern
ממשו את מחלקת HtmlHelper כ-Singleton כדי:
- להבטיח שיש רק מופע אחד פעיל.
- למנוע טעינות חוזרות של קבצי JSON.

#### 6. בניית העץ
- השתמשו בלולאה כדי לבנות עץ HTML מתוך מחרוזות המנותחות.
- התמודדו עם:
  - תגים פתוחים.
  - תגים סגורים (חזרה אל האובייקט ההורה).
  - תגים סגורים עצמאית.
  - תוכן טקסטואלי (עדכון **InnerHtml**).

---

## Html Query

הרכיב Html Query מאפשר לחפש אובייקטים בתוך עץ ה-HTML באמצעות CSS selectors.

### יסודות החיפוש
1. חיפוש לפי שם תג: `"div"`
2. חיפוש לפי מזהה ID: `"#mydiv"`
3. חיפוש לפי Class: `".class-name"`
4. שילוב סלקטורים לקריטריונים מחמירים יותר: `"div#mydiv.class-name"`
5. רווח בין סלקטורים מייצג יחסי צאצא: `"div #mydiv .class-name"`.

#### מחלקת Selector
המחלקה Selector מייצגת שאילתא וכוללת:
- **TagName**
- **Id**
- **Classes** (רשימה)
- יחסי **Parent** ו-**Child** (לבניית עץ בינארי של סלקטורים).

#### שיטות במחלקת HtmlElement
ממשו שיטות לנווט בתוך עץ ה-HTML:

1. **Descendants:**
   - מעבר על העץ החל מהאלמנט הנוכחי.
   - החזרת רשימה שטוחה של כל הצאצאים.

2. **Ancestors:**
   - מעבר למעלה בעץ החל מהאלמנט הנוכחי.
   - החזרת רשימה שטוחה של כל האבות.

3. **חיפוש אלמנטים לפי סלקטור:**
   - השתמשו ברקורסיה לעבור על עץ HtmlElement ועץ Selector בו-זמנית.
   - השתמשו ב-`HashSet` כדי למנוע כפילויות.

---

## בדיקות ואימות

- השוו את תוצאות השאילתא לפלט מהדפדפן באמצעות הפקודה:
  ```javascript
  $$("div .class-name")
  ```
- בצעו אימות מול קוד המקור של ה-HTML (לא תוכן שנוסף דינאמית באמצעות JavaScript).

---

## מקורות נוספים
- **ביטויים רגולריים ב-.NET**: [צפה בסרטון](#)
- **Singleton Design Pattern ב-.NET**: [צפה בסרטון](#)
- **HashSet ב-.NET**: [צפה בסרטון](#)
- **IEnumerable & yield return**: [צפה בסרטון](#)
- **אימות שאילתות בדפדפן**: [צפה בסרטון](#)

---

כלי זה מספק מסגרת חזקה לטיפול ושאילתת נתוני HTML, ומשמש כבסיס ליצירת זחלן או סורק רשת מלא.

בהצלחה! 🎉

