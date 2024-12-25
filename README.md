# HtmlSerializer

## תיאור הפרויקט
פרויקט `HtmlSerializer` הוא כלי לעבודה עם מבנה HTML בצורה מונחית עצמים (OOP). הפרויקט כולל מחלקות לייצוג תגיות HTML, יצירת עץ HTML, חיפוש אלמנטים בעזרת סלקטורים, וניהול תגיות באמצעות תכונות, מזהים (ID) וכיתות (Classes).  

## מאפיינים עיקריים
- **ייצוג תגיות HTML**:  
  מחלקת `HtmlElement` מספקת מודל לייצוג תגיות HTML, כולל תכונות, מזהים, כיתות ותוכן פנימי (InnerHtml).  
- **חיפוש אלמנטים**:  
  ניתן לחפש אלמנטים בעץ HTML באמצעות סלקטורים (כגון `div.class1` או `#id`).  
- **טעינת HTML מתוך URL**:  
  הפרויקט מאפשר לטעון תוכן HTML מאתר אינטרנט ולעבד אותו לעץ תגיות.  
- **ניהול תגיות HTML**:  
  כולל עבודה עם תגיות פשוטות ותגיות Void (ללא תוכן פנימי).  

## קבצים עיקריים
1. **`Selector.cs`**  
   מספקת יכולת לפרש סלקטורים ולהשתמש בהם כדי למצוא תגיות בעץ HTML.

2. **`HtmlElement.cs`**  
   מייצגת תגיות HTML ומספקת יכולות לעבודה עם אבות, צאצאים, ותכונות.

3. **`HtmlHelper.cs`**  
   אחראית לטעינת רשימות של תגיות HTML ותמיכה בתגיות Void מקבצי JSON.

4. **`Program.cs`**  
   משמש כדוגמה ליישום הפרויקט: טעינת תוכן HTML מאתר, בניית עץ HTML, חיפוש אלמנטים והדפסתם.

## שימוש בפרויקט
1. **טעינת תוכן HTML**  
   ניתן לטעון תוכן HTML מאתר אינטרנט בעזרת הפונקציה `Load`:
   ```csharp
   var html = await Load("https://example.com");
   ```

2. **בניית עץ HTML**  
   עץ HTML נבנה באמצעות הפונקציה `BuildHtmlTree`:
   ```csharp
   HtmlElement root = BuildHtmlTree(html);
   ```

3. **חיפוש אלמנטים**  
   ניתן לחפש אלמנטים באמצעות הסלקטור:
   ```csharp
   var matches = root.Query(".className");
   foreach (var match in matches)
   {
       Console.WriteLine($"<Matched Element: {match.Name}>");
   }
   ```

4. **הדפסת עץ HTML**  
   הפונקציה `PrintHtmlTree` מדפיסה את מבנה העץ:
   ```csharp
   PrintHtmlTree(root, 0);
   ```

## דרישות מערכת
- .NET 6.0 ומעלה
- Visual Studio או עורך קוד אחר התומך ב-C#


