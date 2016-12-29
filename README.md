# Repo for Agila Metoder group task

## Grupp 18

Kör kommando i terminalen för att skapa en kopia av projektet:
```git
git clone https://github.com/kevinmartinez/agila-metoder-grupp.git .
```
Efter att ha klonat så kör detta i terminalen:
```git
git fetch
```
```git
git pull
```

Skapa sen en textfil.txt och kör
```git
git status
```
Nu ligger textfil.txt som untracked, vi kan skicka in den i vårt repo än

För att lägga till filen
```git
git add text.txt
```
För att kommentera ändringen
```git
git commit -m "add new textfile"
```
Se status igen, nu är det tomt
```git
git status
```

För att lägga upp filerna i vårt repo, så att de blir synliga i Github:
```git
git push
```

**När ni ska fortsätta att arbeta med projektet**
```git
git fetch && git pull
```
