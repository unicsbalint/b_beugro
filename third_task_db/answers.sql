1. listázd azon userek nevét, akiknek nincs autója és a vezetékneve Kis :
SELECT user.name FROM `user`,user_car WHERE user_car.user = user.id and name LIKE "Kis%" GROUP BY user_car.user HAVING COUNT(user_car.user) = 0;

2.listázd azon userek nevét, akiknek legalább 2 autója van	
SELECT user.name FROM `user`,user_car WHERE user_car.user = user.id GROUP BY user_car.user HAVING COUNT(user_car.user) > 1;

2.1
backlog

3. szúrj be két oszlopot a user táblába: szemelyi_szam , nem
ALTER TABLE user
ADD szemelyi_szam varchar(11);
ALTER TABLE user
ADD nem BIT;
-- 1=férfi 0=nő

4.
INSERT INTO `car`(`brand`, `model`) VALUES ("Volkswagen","Arteon")
-- AI miatt nem kell id-t kitölteni

5. módosítsd a car tábla tartalmában azokat a model-eket Fiesta-ra, ahol a model Focus
UPDATE `car` SET model="Fiesta" WHERE model="Focus";

6. adj hozzá minden userhez egy volkswagen arteont akinek a nevében szerepel "o" vagy "r" betű illetve az id-ja kisebb, mint 10	

INSERT INTO `user_car`(`user`, `car`) 
    SELECT id,(SELECT id FROM `car` where brand="Volkswagen" and model="Arteon")
                     FROM `user` where id < 10 and (name LIKE "%o%" or name LIKE "%r%")

7.biztosítsd a user_car tábla egyediségét index segítségével	

