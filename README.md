# TheHiveMind
## Git Merge from another fork/branch

1. git checkout -b Lugge-master master
2. git pull https://github.com/Lugge/TheHiveMind.git master

3. git checkout master
4. git merge --no-ff Lugge-master
5. git push origin master
