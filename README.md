# Designeo.Eshop

Description: Sample Asp.net core app for managing orders of an eshop. Basic authentication is required, any username will be accepted.

Usage:
* /api/orders -> list of existing orders, no authentication required
* /api/orders/2 -> specific order, accepts GET, POST, PUT, DELETE. 
* /api/orders/2/items -> specific order items, accepts POST, DELETE. 

Zadání je jednoduché, ale celkově jakkoliv malé API s sebou nese velké množství implementačních rozhodnutí, které jsou pro daný projekt globální. Jako například error handling, logging, rozdělení logiky a použité návrhové vzory (repositories, DTOs), stránkování, ETagy a další. Většinu takových rozhodnutí jsem ve svém řešení z časových důvodů vynechal. Zabezpečení jsem implementoval formou nuget balíčku a za pomocí Basic autentikace. Takové řešení je vhodné pouze pro testovací účely, pro produkční použití by bylo vhodné použít robustní řešení (identity server, AD atp.) 

Requirement:

•             Aplikace by měla být v ASP.NET Core 3.0

•             Měla by obsahovat jednoduché API pro vytvoření, editaci a smazání objednávky.

•             Každá objednávka má celkovou cenu.

•             Ke každé objednávce by mělo být možné znovu pomocí API zadat 1-N položek.

•             API by mělo být zabezpečené. Jaké zabezpečení zvolíte nechám na Vás.

•             Volba databáze je také na Vás, jako ORM ale použijte Entity Framework Core.

•             Použijte verzovací systém.

