using Microsoft.EntityFrameworkCore.Migrations;

namespace Yoma.Core.Infrastructure.Database.Migrations
{
    internal class _20230728053910_ApplicationDb_Initial_Seeding
    {
        internal class ApplicationDb_Initial_Seeding
        {
            internal static void Seed(MigrationBuilder migrationBuilder)
            {
                migrationBuilder.InsertData(
                table: "ProviderType",
                columns: new[] { "Id", "Name", "DateCreated" },
                values: new object[,]
                {
                    {"A3BCAA03-B31C-4830-AAE8-06BBA701D3F0","Opportunity",DateTimeOffset.Now}
                    ,
                    {"6FB02F6F-34FE-4E6E-9094-2E3B54115235","Educational",DateTimeOffset.Now}
                    ,
                    {"D2987F9F-8CC8-4576-AF09-C01213A1435E","Marketplace",DateTimeOffset.Now}
                },
                schema: "lookup");


                migrationBuilder.InsertData(
                table: "Gender",
                columns: new[] { "Id", "Name", "DateCreated" },
                values: new object[,]
                {
                    {"6DBD31E9-5196-49CA-8D3B-8354A9BFF996","Male",DateTimeOffset.Now}
                    ,
                    {"6342C98A-0572-4E6A-A4FB-A1AEAFD3C053","Female",DateTimeOffset.Now}
                    ,
                    {"1BDCBFAD-67AA-4556-A326-5D263761F920","Transgender Male",DateTimeOffset.Now}
                    ,
                    {"0BE24DF8-D4C5-453C-AADF-EEDF673E1FE3","Transgender Female",DateTimeOffset.Now}
                    ,
                    {"26BA24A5-9209-48B2-A885-95C43EF142B5","Unknown",DateTimeOffset.Now}
                },
                schema: "lookup");

                migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "Name", "CodeAlpha2", "CodeAlpha3", "CodeNumeric", "DateCreated" },
                values: new object[,]
                {
                    {"a0d029b2-49ca-4e89-81aa-8d06be5d2241","Afghanistan","AF","AFG","4",DateTimeOffset.Now}
                    ,
                    {"fb8c57b0-255a-4528-ae87-4b324f47a4d5","Åland Islands","AX","ALA","248",DateTimeOffset.Now}
                    ,
                    {"72301ed7-691b-4258-8363-c94c88918e7c","Albania","AL","ALB","8",DateTimeOffset.Now}
                    ,
                    {"d21d35f8-d319-46bc-8818-c266ffb8d32c","Algeria","DZ","DZA","12",DateTimeOffset.Now}
                    ,
                    {"e958aaef-22ad-42a9-8017-d4ed5f9231a0","American Samoa","AS","ASM","16",DateTimeOffset.Now}
                    ,
                    {"3036f767-1b2d-4dc1-8b31-0edc75e2235d","Andorra","AD","AND","20",DateTimeOffset.Now}
                    ,
                    {"37c77fb4-7ac6-4994-880f-c01296832a0e","Angola","AO","AGO","24",DateTimeOffset.Now}
                    ,
                    {"3b3a8faf-aa48-4884-9b0b-b865ce305f13","Anguilla","AI","AIA","660",DateTimeOffset.Now}
                    ,
                    {"a6f1c4c6-afee-4224-83a1-013061b433b6","Antarctica","AQ","ATA","10",DateTimeOffset.Now}
                    ,
                    {"5cff2dfc-1bdd-44cb-a392-f8b75d1c9495","Antigua and Barbuda","AG","ATG","28",DateTimeOffset.Now}
                    ,
                    {"d564c13e-0715-4f44-aeb3-21bdfe4d2219","Argentina","AR","ARG","32",DateTimeOffset.Now}
                    ,
                    {"58e44cce-310a-4d5e-a207-253597705099","Armenia","AM","ARM","51",DateTimeOffset.Now}
                    ,
                    {"a6e08be7-096d-4e3d-9e94-9fbc9c8aa8df","Aruba","AW","ABW","533",DateTimeOffset.Now}
                    ,
                    {"bd1296b1-e347-40ad-9ea7-4b48f399cf65","Australia","AU","AUS","36",DateTimeOffset.Now}
                    ,
                    {"146a7383-f1e0-4a3d-a9a0-edbcacc2a6ef","Austria","AT","AUT","40",DateTimeOffset.Now}
                    ,
                    {"003358f8-22ca-4301-86b7-922ed14716ee","Azerbaijan","AZ","AZE","31",DateTimeOffset.Now}
                    ,
                    {"055616ce-4d23-4dec-901c-60259e5ee55a","Bahamas","BS","BHS","44",DateTimeOffset.Now}
                    ,
                    {"1ce96971-155c-44e2-9ed2-6556bcff9557","Bahrain","BH","BHR","48",DateTimeOffset.Now}
                    ,
                    {"315d42dc-cff8-48be-8443-786b4b785615","Bangladesh","BD","BGD","50",DateTimeOffset.Now}
                    ,
                    {"19f99b77-98c7-4981-a480-ca90a015e32f","Barbados","BB","BRB","52",DateTimeOffset.Now}
                    ,
                    {"9f4eea75-3e64-405f-8f17-7e9e6fd0745a","Belarus","BY","BLR","112",DateTimeOffset.Now}
                    ,
                    {"3ba221d6-e01a-48ae-9f90-89a226856b6f","Belgium","BE","BEL","56",DateTimeOffset.Now}
                    ,
                    {"8af05ae0-feb1-4d0d-b833-1859a9ace61e","Belize","BZ","BLZ","84",DateTimeOffset.Now}
                    ,
                    {"82b2cd7f-de0f-4465-ab7d-434d34658898","Benin","BJ","BEN","204",DateTimeOffset.Now}
                    ,
                    {"528419a5-e076-4c3d-b79c-6adcea69ed9a","Bermuda","BM","BMU","60",DateTimeOffset.Now}
                    ,
                    {"31b8bb88-6039-41bc-866b-ac1f9c7a213a","Bhutan","BT","BTN","64",DateTimeOffset.Now}
                    ,
                    {"ca0beff2-f231-442a-bc8b-22c8f4e7aaed","Bolivia","BO","BOL","68",DateTimeOffset.Now}
                    ,
                    {"99dd1f36-165d-49fd-b6da-e5ec4c91a057","Bosnia and Herzegovina","BA","BIH","70",DateTimeOffset.Now}
                    ,
                    {"da255132-2f8b-4e4a-b8ba-4678004f4e63","Botswana","BW","BWA","72",DateTimeOffset.Now}
                    ,
                    {"3e9ae3e9-95fa-41a3-b7a9-351b9a6c1722","Bouvet Island","BV","BVT","74",DateTimeOffset.Now}
                    ,
                    {"512ae770-5150-4a8c-bd64-f06b80e1264f","Brazil","BR","BRA","76",DateTimeOffset.Now}
                    ,
                    {"3ad78a12-bca2-4b89-89c0-bedceb5c080e","British Indian Ocean Territory","IO","IOT","86",DateTimeOffset.Now}
                    ,
                    {"de05ffcc-5900-4a8d-bad1-6d87745bbfd6","British Virgin Islands","VG","VGB","92",DateTimeOffset.Now}
                    ,
                    {"9ecb840b-85ec-4ab0-b368-53a59dc38b5c","Brunei","BN","BRN","96",DateTimeOffset.Now}
                    ,
                    {"025b640b-80df-4ce2-a018-1fefff75aa5f","Bulgaria","BG","BGR","100",DateTimeOffset.Now}
                    ,
                    {"dc758e1f-7136-4494-91b8-e43ceecf35ca","Burkina Faso","BF","BFA","854",DateTimeOffset.Now}
                    ,
                    {"ed7874ea-8b58-4ae7-8d97-223ca2884f7c","Burundi","BI","BDI","108",DateTimeOffset.Now}
                    ,
                    {"dc8deb14-d7a6-4829-8af2-6b5d59e62450","Cambodia","KH","KHM","116",DateTimeOffset.Now}
                    ,
                    {"9a5cca1a-c9bd-44db-9e8b-4717bfbe3bda","Cameroon","CM","CMR","120",DateTimeOffset.Now}
                    ,
                    {"0793538f-e7cc-4a41-80a4-8fd22660c7da","Canada","CA","CAN","124",DateTimeOffset.Now}
                    ,
                    {"22ca70e2-9d6e-440b-b044-e09d678fa5bd","Cape Verde","CV","CPV","132",DateTimeOffset.Now}
                    ,
                    {"2d2b8fc6-15a3-4875-9624-6f6dd3e120e6","Caribbean Netherlands","BQ","BES","535",DateTimeOffset.Now}
                    ,
                    {"db12409c-3fbf-4788-a44e-b1a7581c3da4","Cayman Islands","KY","CYM","136",DateTimeOffset.Now}
                    ,
                    {"83ada848-fecf-4667-aacc-6876b1fcbb14","Central African Republic","CF","CAF","140",DateTimeOffset.Now}
                    ,
                    {"9c1d3834-3af9-4e8e-8bc0-d2d2b3fb628e","Chad","TD","TCD","148",DateTimeOffset.Now}
                    ,
                    {"a3fdbeb2-4d00-4536-aab1-352848d24637","Chile","CL","CHL","152",DateTimeOffset.Now}
                    ,
                    {"a2ace964-a183-426f-9e96-2ecd2c223b48","China","CN","CHN","156",DateTimeOffset.Now}
                    ,
                    {"337d4bb8-48e1-4300-8b55-ebcf3e1f959d","Christmas Island","CX","CXR","162",DateTimeOffset.Now}
                    ,
                    {"9d17d6f5-b35d-428d-bf3d-e312fee73edf","Cocos (Keeling) Islands","CC","CCK","166",DateTimeOffset.Now}
                    ,
                    {"fa88ad3e-d64f-4403-aee8-44c583964a81","Colombia","CO","COL","170",DateTimeOffset.Now}
                    ,
                    {"d2ea25a6-d58c-4348-9821-f5303942c7fa","Comoros","KM","COM","174",DateTimeOffset.Now}
                    ,
                    {"b8e57c86-e4e6-4ebc-b296-669784209d41","DR Congo","CD","COD","180",DateTimeOffset.Now}
                    ,
                    {"2a456ea2-c5fb-4e83-bfc4-c162afbeaf0e","Cook Islands","CK","COK","184",DateTimeOffset.Now}
                    ,
                    {"fc1dac98-4662-48e5-a38b-02dec04e61ff","Costa Rica","CR","CRI","188",DateTimeOffset.Now}
                    ,
                    {"7bf77579-31d8-4711-b1dc-37c97899b12b","Croatia","HR","HRV","191",DateTimeOffset.Now}
                    ,
                    {"bff0419a-f0c9-4702-a9f6-873e81d76bbf","Cuba","CU","CUB","192",DateTimeOffset.Now}
                    ,
                    {"6653fb7f-a878-4cc3-8655-71cacd1f4d0f","Curaçao","CW","CUW","531",DateTimeOffset.Now}
                    ,
                    {"df6ff353-bf88-4f06-869a-4ed5488ede8c","Cyprus","CY","CYP","196",DateTimeOffset.Now}
                    ,
                    {"2554aaa4-a2fc-4152-a988-72e4e934dc63","Czechia","CZ","CZE","203",DateTimeOffset.Now}
                    ,
                    {"9083cc27-2069-4f6e-aa06-e816bb99bf1b","Denmark","DK","DNK","208",DateTimeOffset.Now}
                    ,
                    {"a687730f-8adb-4296-8b79-aaef307e28f9","Djibouti","DJ","DJI","262",DateTimeOffset.Now}
                    ,
                    {"2535926f-7e75-4d55-9cb6-39df104fb3c3","Dominica","DM","DMA","212",DateTimeOffset.Now}
                    ,
                    {"884f1d96-f0a3-4750-a7b7-825946f8763a","Dominican Republic","DO","DOM","214",DateTimeOffset.Now}
                    ,
                    {"51ca4551-1104-47a1-a70f-a998a3f706ea","Ecuador","EC","ECU","218",DateTimeOffset.Now}
                    ,
                    {"6277c849-2b45-4958-923f-966d88a8296e","Egypt","EG","EGY","818",DateTimeOffset.Now}
                    ,
                    {"3106aa73-3daf-46d0-a57b-9d7dc4998ee8","El Salvador","SV","SLV","222",DateTimeOffset.Now}
                    ,
                    {"6696ed33-d816-4d83-9ed4-f64bb5144425","Equatorial Guinea","GQ","GNQ","226",DateTimeOffset.Now}
                    ,
                    {"860bf436-6a6b-437c-9940-ca8f60374a82","Eritrea","ER","ERI","232",DateTimeOffset.Now}
                    ,
                    {"1bcbc637-9d6c-4aea-af83-8ae1a5012969","Estonia","EE","EST","233",DateTimeOffset.Now}
                    ,
                    {"2e1303cd-e3e3-4458-9186-3a289a4657eb","Ethiopia","ET","ETH","231",DateTimeOffset.Now}
                    ,
                    {"9e652f7f-7371-4eaa-86cf-63e8724aaf9a","Falkland Islands","FK","FLK","238",DateTimeOffset.Now}
                    ,
                    {"97a6cb5c-fd95-4679-b72b-7da032ba26e9","Faroe Islands","FO","FRO","234",DateTimeOffset.Now}
                    ,
                    {"93aea0df-6e02-49a8-b4b1-a038525cede2","Fiji","FJ","FJI","242",DateTimeOffset.Now}
                    ,
                    {"d0a6a7a2-b7f6-4f69-8f0c-9f21a0e71faf","Finland","FI","FIN","246",DateTimeOffset.Now}
                    ,
                    {"db8f6d2d-e71a-42bf-8f97-e05a2be812f3","France","FR","FRA","250",DateTimeOffset.Now}
                    ,
                    {"d1748139-7594-4b54-bd06-b5faf51bcdb3","French Guiana","GF","GUF","254",DateTimeOffset.Now}
                    ,
                    {"42debfc6-9c21-4f77-b085-6e7c7a650bb2","French Polynesia","PF","PYF","258",DateTimeOffset.Now}
                    ,
                    {"ac7ad8fc-c272-4894-b9f9-24553b7af384","French Southern and Antarctic Lands","TF","ATF","260",DateTimeOffset.Now}
                    ,
                    {"3a0ea176-9675-4586-b7b9-7dbcb0921f70","Gabon","GA","GAB","266",DateTimeOffset.Now}
                    ,
                    {"3c93543e-30f3-4eb0-8f76-17061ff7834c","Gambia","GM","GMB","270",DateTimeOffset.Now}
                    ,
                    {"213a2c5b-717a-46b5-9f9d-67942b21f6e8","Georgia","GE","GEO","268",DateTimeOffset.Now}
                    ,
                    {"193f7866-2d33-424c-98e8-c296aabb9fa9","Germany","DE","DEU","276",DateTimeOffset.Now}
                    ,
                    {"38e90e00-7486-4a2f-9f51-62e72b942e1b","Ghana","GH","GHA","288",DateTimeOffset.Now}
                    ,
                    {"cb699b96-dfdb-4482-9f6f-5d8c7f003048","Gibraltar","GI","GIB","292",DateTimeOffset.Now}
                    ,
                    {"1dc18718-eb7a-4cfe-a1c1-082e32bc0b01","Greece","GR","GRC","300",DateTimeOffset.Now}
                    ,
                    {"3baf616c-32e1-46b2-9f4d-22d9ca6cbb4d","Greenland","GL","GRL","304",DateTimeOffset.Now}
                    ,
                    {"7332b0a2-3e7c-426f-800a-75f9353d86a1","Grenada","GD","GRD","308",DateTimeOffset.Now}
                    ,
                    {"7c0d3c6c-bbab-4360-8ec9-82de8d5e791d","Guadeloupe","GP","GLP","312",DateTimeOffset.Now}
                    ,
                    {"7270e9ec-4d0e-4bcb-be35-adf5d363e791","Guam","GU","GUM","316",DateTimeOffset.Now}
                    ,
                    {"a0d52371-8435-4d55-9c8a-ab73d184f561","Guatemala","GT","GTM","320",DateTimeOffset.Now}
                    ,
                    {"ded22628-edc0-4f8b-a4bf-8bc27fcc0777","Guernsey","GG","GGY","831",DateTimeOffset.Now}
                    ,
                    {"3e2e1a75-8f76-4330-80d9-c135fad46e35","Guinea-Bissau","GW","GNB","624",DateTimeOffset.Now}
                    ,
                    {"2e3b19d4-10d6-4af7-a446-11d8ef6d48da","Guinea","GN","GIN","324",DateTimeOffset.Now}
                    ,
                    {"374dc265-08dc-4492-80a3-a4de0870445f","Guyana","GY","GUY","328",DateTimeOffset.Now}
                    ,
                    {"fac56cfa-21f5-4b8d-9bcb-c50330f25f15","Haiti","HT","HTI","332",DateTimeOffset.Now}
                    ,
                    {"49fb1ca1-5263-4a10-bd5c-2764b7c12b7d","Heard Island and McDonald Islands","HM","HMD","334",DateTimeOffset.Now}
                    ,
                    {"4e1aef48-d3f0-493d-92c7-21261466531c","Honduras","HN","HND","340",DateTimeOffset.Now}
                    ,
                    {"57b17dee-b014-4c82-8140-34652dcbcf13","Hong Kong","HK","HKG","344",DateTimeOffset.Now}
                    ,
                    {"da435307-ba56-43dc-9a8f-f56222affebf","Hungary","HU","HUN","348",DateTimeOffset.Now}
                    ,
                    {"0c5a46af-576d-430a-b601-22778ba00af2","Iceland","IS","ISL","352",DateTimeOffset.Now}
                    ,
                    {"189b0871-8b24-419e-9f35-bfb028dbd005","India","IN","IND","356",DateTimeOffset.Now}
                    ,
                    {"a0aea11b-aaed-4754-828f-ebc71d08c31f","Indonesia","ID","IDN","360",DateTimeOffset.Now}
                    ,
                    {"cc5da383-5f97-4247-b9fe-7e9b6998d52c","Iran","IR","IRN","364",DateTimeOffset.Now}
                    ,
                    {"a2a07201-b346-4e63-8fc8-0e044f6a3100","Iraq","IQ","IRQ","368",DateTimeOffset.Now}
                    ,
                    {"cbd07fd5-876d-408e-9009-f5aa16a89a3e","Ireland","IE","IRL","372",DateTimeOffset.Now}
                    ,
                    {"d11e7129-ccb3-436f-958e-40ee68250e57","Isle of Man","IM","IMN","833",DateTimeOffset.Now}
                    ,
                    {"e04e5e80-ac0d-4864-b73a-16f92be03b98","Israel","IL","ISR","376",DateTimeOffset.Now}
                    ,
                    {"8051f4c2-7266-45cc-94d0-cda4e4b6b2e8","Italy","IT","ITA","380",DateTimeOffset.Now}
                    ,
                    {"cb31598e-086d-41fe-9b28-517fdc05086a","Ivory Coast","CI","CIV","384",DateTimeOffset.Now}
                    ,
                    {"7a424b4d-98e3-4b2d-ac6a-f91b64e9de9c","Jamaica","JM","JAM","388",DateTimeOffset.Now}
                    ,
                    {"6e40b858-23b0-49bb-891b-38fdd48390a1","Japan","JP","JPN","392",DateTimeOffset.Now}
                    ,
                    {"ff3994f7-30fa-437f-96ad-ce02c73819ff","Jersey","JE","JEY","832",DateTimeOffset.Now}
                    ,
                    {"3b14cbae-18ee-4061-9b87-df59527c648c","Jordan","JO","JOR","400",DateTimeOffset.Now}
                    ,
                    {"a96c4622-1155-4dc8-82f8-d4dd3765e9d2","Kazakhstan","KZ","KAZ","398",DateTimeOffset.Now}
                    ,
                    {"a0573190-e86f-489e-8e05-87fdba1d442f","Kenya","KE","KEN","404",DateTimeOffset.Now}
                    ,
                    {"858cee80-e6e8-4047-954d-e2eeb9d69c29","Kiribati","KI","KIR","296",DateTimeOffset.Now}
                    ,
                    {"5ddcbf09-25d4-4c85-80a1-f888f0d91a59","Kuwait","KW","KWT","414",DateTimeOffset.Now}
                    ,
                    {"455b45ad-26b5-4e95-8a64-d56865b1524d","Kyrgyzstan","KG","KGZ","417",DateTimeOffset.Now}
                    ,
                    {"c8daf0db-2ee5-487c-ad4d-daa59e16bf5f","Laos","LA","LAO","418",DateTimeOffset.Now}
                    ,
                    {"e6659376-9bb2-42a5-9c9f-5bae7ef585bd","Latvia","LV","LVA","428",DateTimeOffset.Now}
                    ,
                    {"87395857-20ea-4d19-ab00-768e04b8c763","Lebanon","LB","LBN","422",DateTimeOffset.Now}
                    ,
                    {"8ac6e42a-c3f1-479d-b335-064138d3892a","Lesotho","LS","LSO","426",DateTimeOffset.Now}
                    ,
                    {"224ea6e3-03ee-49f0-85a3-aa5aaa664673","Liberia","LR","LBR","430",DateTimeOffset.Now}
                    ,
                    {"4475bb0b-d833-4838-a527-f457b4aeae03","Libya","LY","LBY","434",DateTimeOffset.Now}
                    ,
                    {"826f55ae-26d9-4e92-8ff7-cdb6baa01fc4","Liechtenstein","LI","LIE","438",DateTimeOffset.Now}
                    ,
                    {"d15ba910-8685-4f48-912f-4b7f4cec339d","Lithuania","LT","LTU","440",DateTimeOffset.Now}
                    ,
                    {"217abebb-d717-4b0b-9da8-08f7742802a0","Luxembourg","LU","LUX","442",DateTimeOffset.Now}
                    ,
                    {"61e8acf6-fe38-449b-a80c-b2d2de0a06a2","Macau","MO","MAC","446",DateTimeOffset.Now}
                    ,
                    {"88550902-d519-480f-8d3a-1537ab3ceea9","Madagascar","MG","MDG","450",DateTimeOffset.Now}
                    ,
                    {"1bfde6a1-6b2a-4c0e-9be6-5c7c6f2632e9","Malawi","MW","MWI","454",DateTimeOffset.Now}
                    ,
                    {"daabae09-2df5-47b0-8579-3ad458ee76fc","Malaysia","MY","MYS","458",DateTimeOffset.Now}
                    ,
                    {"c37404c9-40b1-49ef-b451-49e0c764d5fd","Maldives","MV","MDV","462",DateTimeOffset.Now}
                    ,
                    {"f3434e8c-0397-419c-bdd9-a98d7752bb9c","Mali","ML","MLI","466",DateTimeOffset.Now}
                    ,
                    {"c8ed836c-fecb-4d51-b137-cc77c9253dc5","Malta","MT","MLT","470",DateTimeOffset.Now}
                    ,
                    {"61b62814-51ed-4cb6-be6a-d7b5ae48b9f9","Marshall Islands","MH","MHL","584",DateTimeOffset.Now}
                    ,
                    {"8006aab3-92ef-423d-a3d4-c5e8bf0d9596","Martinique","MQ","MTQ","474",DateTimeOffset.Now}
                    ,
                    {"60ada253-ce25-4d2b-8fdd-f2e6f80c372a","Mauritania","MR","MRT","478",DateTimeOffset.Now}
                    ,
                    {"d18b427a-b963-46c5-aec1-245a11c8b882","Mauritius","MU","MUS","480",DateTimeOffset.Now}
                    ,
                    {"f22d2cdb-5c0e-49cc-8b5a-9a25c5273ae7","Mayotte","YT","MYT","175",DateTimeOffset.Now}
                    ,
                    {"25b0fa7e-6a4f-467a-b512-cc817d9d0087","Mexico","MX","MEX","484",DateTimeOffset.Now}
                    ,
                    {"6bd42521-056b-4a84-9c30-ef2dafc778ab","Micronesia","FM","FSM","583",DateTimeOffset.Now}
                    ,
                    {"dd5879da-d363-4619-a478-4c3686a9d457","Moldova","MD","MDA","498",DateTimeOffset.Now}
                    ,
                    {"7a8b7e61-f049-4f57-9c6b-f139a65e979a","Monaco","MC","MCO","492",DateTimeOffset.Now}
                    ,
                    {"f98853fc-da8d-4d92-ac4d-b5ab5128af88","Mongolia","MN","MNG","496",DateTimeOffset.Now}
                    ,
                    {"ca9fc03d-382e-4aff-8c60-99531037fb5e","Montenegro","ME","MNE","499",DateTimeOffset.Now}
                    ,
                    {"66c0f758-f6de-41b2-abb3-302cb103caf2","Montserrat","MS","MSR","500",DateTimeOffset.Now}
                    ,
                    {"87ef8004-0407-4779-8c5d-f5fd45d2dc75","Morocco","MA","MAR","504",DateTimeOffset.Now}
                    ,
                    {"96ff7073-fde0-4c70-a56b-b7e1256568c0","Mozambique","MZ","MOZ","508",DateTimeOffset.Now}
                    ,
                    {"4f15d554-5086-4d5c-8b81-592b65428eb5","Myanmar","MM","MMR","104",DateTimeOffset.Now}
                    ,
                    {"433741db-ed8a-45db-b5fc-af0f96399a10","Namibia","NA","NAM","516",DateTimeOffset.Now}
                    ,
                    {"938c909b-9a86-4a3c-8a7e-8ae7e77b9e50","Nauru","NR","NRU","520",DateTimeOffset.Now}
                    ,
                    {"84e6cd7e-2ba6-4008-a19e-06b2cf464776","Nepal","NP","NPL","524",DateTimeOffset.Now}
                    ,
                    {"57b73f36-ce3e-421f-ad41-3ec7939f2faa","Netherlands","NL","NLD","528",DateTimeOffset.Now}
                    ,
                    {"49afda12-15c3-4fc5-8b93-e12818afb78d","New Caledonia","NC","NCL","540",DateTimeOffset.Now}
                    ,
                    {"7a0898e4-dcac-456d-a469-c16ed247bca8","New Zealand","NZ","NZL","554",DateTimeOffset.Now}
                    ,
                    {"866b51be-af49-4f6e-bf88-13bc18329f18","Nicaragua","NI","NIC","558",DateTimeOffset.Now}
                    ,
                    {"2ba19f1d-998f-40ee-9af6-c6ab562e3040","Nigeria","NG","NGA","566",DateTimeOffset.Now}
                    ,
                    {"a1e7fc37-9a98-4f38-a2ed-9e67ab6f104b","Niger","NE","NER","562",DateTimeOffset.Now}
                    ,
                    {"a37a630f-d2e7-4ec0-a858-15d42675e0e5","Niue","NU","NIU","570",DateTimeOffset.Now}
                    ,
                    {"abffbc54-0791-45cc-b8bb-aa582928f607","Norfolk Island","NF","NFK","574",DateTimeOffset.Now}
                    ,
                    {"d0c3560b-4d83-4923-aa14-c69f2e1b1410","Northern Mariana Islands","MP","MNP","580",DateTimeOffset.Now}
                    ,
                    {"490591d9-c44a-4960-acce-3d1cb126881c","North Korea","KP","PRK","408",DateTimeOffset.Now}
                    ,
                    {"b42536ca-fbac-4412-bf62-da5f262f1213","North Macedonia","MK","MKD","807",DateTimeOffset.Now}
                    ,
                    {"6c160616-7c35-4fba-8b06-a982a888c57b","Norway","NO","NOR","578",DateTimeOffset.Now}
                    ,
                    {"fa540e8c-d937-437c-9595-444593d74d83","Oman","OM","OMN","512",DateTimeOffset.Now}
                    ,
                    {"3fc4e23c-0316-470e-a89f-cee6ccf60558","Pakistan","PK","PAK","586",DateTimeOffset.Now}
                    ,
                    {"594e2644-46dd-4e45-904c-0ee5ea46e12e","Palau","PW","PLW","585",DateTimeOffset.Now}
                    ,
                    {"a75e2a12-59c1-4d2a-bc2d-e25bba4ce88e","Palestine","PS","PSE","275",DateTimeOffset.Now}
                    ,
                    {"823063a7-3e0b-4995-be71-6b42659a5fe0","Panama","PA","PAN","591",DateTimeOffset.Now}
                    ,
                    {"022a18d8-f90c-427e-b424-e3edb636f527","Papua New Guinea","PG","PNG","598",DateTimeOffset.Now}
                    ,
                    {"d4dce7eb-97d1-4e16-84c2-950a8e0eb0a1","Paraguay","PY","PRY","600",DateTimeOffset.Now}
                    ,
                    {"6731825b-e068-456d-bb1a-376f3f99653c","Peru","PE","PER","604",DateTimeOffset.Now}
                    ,
                    {"a8beebbd-cbdb-4693-9350-a224e61793fe","Philippines","PH","PHL","608",DateTimeOffset.Now}
                    ,
                    {"16b20a33-686b-491e-b7fb-c6c3866c8644","Pitcairn Islands","PN","PCN","612",DateTimeOffset.Now}
                    ,
                    {"e72f9ea5-d3bd-44d0-8406-efb2b76c722a","Poland","PL","POL","616",DateTimeOffset.Now}
                    ,
                    {"ec0171f1-5a34-4428-bbf0-c8f2ce01620d","Portugal","PT","PRT","620",DateTimeOffset.Now}
                    ,
                    {"26126a25-e870-47cc-b2f1-9da0a90e83a5","Puerto Rico","PR","PRI","630",DateTimeOffset.Now}
                    ,
                    {"1b3048b2-d950-46ea-95ac-ac6960040f1f","Qatar","QA","QAT","634",DateTimeOffset.Now}
                    ,
                    {"58a14d6d-2b26-44f4-9716-f330d4edc83c","Republic of the Congo","CG","COG","178",DateTimeOffset.Now}
                    ,
                    {"cd054300-3d81-4c6f-8734-cf1d8c0c671d","Réunion","RE","REU","638",DateTimeOffset.Now}
                    ,
                    {"83c34cf2-a42c-41b6-be63-b4df5c51a4f7","Romania","RO","ROU","642",DateTimeOffset.Now}
                    ,
                    {"6b92215d-32db-4014-83de-64e44d9a652f","Russia","RU","RUS","643",DateTimeOffset.Now}
                    ,
                    {"b46ed2bd-45f9-495a-be1e-264dd2a73bd6","Rwanda","RW","RWA","646",DateTimeOffset.Now}
                    ,
                    {"8499fbe8-6462-43c5-9714-af6e8f51aaf9","Saint Barthélemy","BL","BLM","652",DateTimeOffset.Now}
                    ,
                    {"22ed84ad-10db-4112-8946-75dcf65be329","Saint Helena, Ascension and Tristan da Cunha","SH","SHN","654",DateTimeOffset.Now}
                    ,
                    {"38233ba2-6f16-4adb-bb80-3aff58a9d98e","Saint Kitts and Nevis","KN","KNA","659",DateTimeOffset.Now}
                    ,
                    {"a459954c-d665-4424-a8e2-79eb63c74c0f","Saint Lucia","LC","LCA","662",DateTimeOffset.Now}
                    ,
                    {"13746d28-ac9e-49d3-8cb8-390eed4fe5c4","Saint Martin","MF","MAF","663",DateTimeOffset.Now}
                    ,
                    {"7626ac97-026a-40c5-b8b7-f332d8b3e1df","Saint Pierre and Miquelon","PM","SPM","666",DateTimeOffset.Now}
                    ,
                    {"dcc1db18-3699-4a65-8488-5a4f5320b80d","Saint Vincent and the Grenadines","VC","VCT","670",DateTimeOffset.Now}
                    ,
                    {"716ee38a-6251-4f00-a48e-9c29d3cafb8c","Samoa","WS","WSM","882",DateTimeOffset.Now}
                    ,
                    {"124e5ac7-fd14-46be-a955-b6f161f5cb35","San Marino","SM","SMR","674",DateTimeOffset.Now}
                    ,
                    {"aa751bf0-319a-4beb-b4e4-7256d5f1a213","São Tomé and Príncipe","ST","STP","678",DateTimeOffset.Now}
                    ,
                    {"f612be2e-777c-4c48-b871-15960de263ec","Saudi Arabia","SA","SAU","682",DateTimeOffset.Now}
                    ,
                    {"dd273bda-e19e-459f-80b4-8e7aa21cef75","Senegal","SN","SEN","686",DateTimeOffset.Now}
                    ,
                    {"3ec2fb85-766c-4b3e-80a3-e3c75344e764","Serbia","RS","SRB","688",DateTimeOffset.Now}
                    ,
                    {"cc93f900-1b0c-4f28-917b-85030a478d79","Seychelles","SC","SYC","690",DateTimeOffset.Now}
                    ,
                    {"20ec9de4-7a0f-4c97-a72f-705774993b64","Sierra Leone","SL","SLE","694",DateTimeOffset.Now}
                    ,
                    {"c13e8f01-0b39-406e-8a5f-acc451c08777","Singapore","SG","SGP","702",DateTimeOffset.Now}
                    ,
                    {"b91a9dd8-0de6-4737-a1de-dabceceacefe","Sint Maarten","SX","SXM","534",DateTimeOffset.Now}
                    ,
                    {"71328869-1bd4-4758-a6f3-14f276ff9a15","Slovakia","SK","SVK","703",DateTimeOffset.Now}
                    ,
                    {"c68271a5-7e4d-4635-acf2-b72d831cd614","Slovenia","SI","SVN","705",DateTimeOffset.Now}
                    ,
                    {"f53d34e3-da67-497a-b120-d2032609b36e","Solomon Islands","SB","SLB","90",DateTimeOffset.Now}
                    ,
                    {"24763389-7c4c-4438-81e3-aa730e27bde7","Somalia","SO","SOM","706",DateTimeOffset.Now}
                    ,
                    {"bab77522-001d-42cb-a1ee-394e16ee5613","South Africa","ZA","ZAF","710",DateTimeOffset.Now}
                    ,
                    {"3b6d4a37-55fd-4997-b0f4-fc882ecdbba8","South Georgia","GS","SGS","239",DateTimeOffset.Now}
                    ,
                    {"4af9cc44-18de-4711-a63c-dce425975954","South Korea","KR","KOR","410",DateTimeOffset.Now}
                    ,
                    {"21bb56c8-782c-45cd-994e-fe387f8b0a2c","South Sudan","SS","SSD","728",DateTimeOffset.Now}
                    ,
                    {"a5a45205-c646-4336-9e61-3259262a02f0","Spain","ES","ESP","724",DateTimeOffset.Now}
                    ,
                    {"114232b3-c958-4f9e-9c14-7d99a6249e71","Sri Lanka","LK","LKA","144",DateTimeOffset.Now}
                    ,
                    {"db2926f9-150e-4adf-bebb-9b29d455684e","Sudan","SD","SDN","729",DateTimeOffset.Now}
                    ,
                    {"210c3ab2-89e4-42af-ac1e-9a21f34d5c71","Suriname","SR","SUR","740",DateTimeOffset.Now}
                    ,
                    {"ba0dcec6-e7da-46fe-9cff-f2d6d5e5f074","Svalbard and Jan Mayen","SJ","SJM","744",DateTimeOffset.Now}
                    ,
                    {"3ebb2e9d-085a-4d9e-8a10-15b676cda6e0","Eswatini","SZ","SWZ","748",DateTimeOffset.Now}
                    ,
                    {"98a66f78-1949-4835-8d1e-00bee53e36c6","Sweden","SE","SWE","752",DateTimeOffset.Now}
                    ,
                    {"adcdcc45-a295-4d92-88d4-c318a256e714","Switzerland","CH","CHE","756",DateTimeOffset.Now}
                    ,
                    {"1db37e3f-799e-430e-b504-910f37a68df9","Syria","SY","SYR","760",DateTimeOffset.Now}
                    ,
                    {"0808098b-9994-49bb-a416-02efc74d69ad","Taiwan","TW","TWN","158",DateTimeOffset.Now}
                    ,
                    {"e3807344-73e5-41fb-a0f7-6cf1e02c63cf","Tajikistan","TJ","TJK","762",DateTimeOffset.Now}
                    ,
                    {"0932e8a6-653f-4be8-b08f-048a0e7a4231","Tanzania","TZ","TZA","834",DateTimeOffset.Now}
                    ,
                    {"83173f45-c4b1-4ccc-a967-c84f23685a35","Thailand","TH","THA","764",DateTimeOffset.Now}
                    ,
                    {"6ed51acb-226d-419a-8131-047d6bf94e15","Timor-Leste","TL","TLS","626",DateTimeOffset.Now}
                    ,
                    {"190e6963-3b52-4a98-9e7e-15d737b7ad3b","Togo","TG","TGO","768",DateTimeOffset.Now}
                    ,
                    {"9b94a444-0ca8-4622-9ff4-e9590c0fe90e","Tokelau","TK","TKL","772",DateTimeOffset.Now}
                    ,
                    {"dd636575-4adf-4ab3-bbe7-b7856c5e28a1","Tonga","TO","TON","776",DateTimeOffset.Now}
                    ,
                    {"adfe5723-b858-492c-b110-bb0807a11da1","Trinidad and Tobago","TT","TTO","780",DateTimeOffset.Now}
                    ,
                    {"cceffd4e-69dd-4afa-8106-803485970177","Tunisia","TN","TUN","788",DateTimeOffset.Now}
                    ,
                    {"8d266a94-4100-40df-bfc5-c5eac4fc5d3f","Turkey","TR","TUR","792",DateTimeOffset.Now}
                    ,
                    {"ab942134-c661-4ac2-98dd-dfed69969d42","Turkmenistan","TM","TKM","795",DateTimeOffset.Now}
                    ,
                    {"147aca9c-5e54-4076-9877-6e35ec206b41","Turks and Caicos Islands","TC","TCA","796",DateTimeOffset.Now}
                    ,
                    {"229c66d5-57e8-4b2a-b86d-02434c78e1e6","Tuvalu","TV","TUV","798",DateTimeOffset.Now}
                    ,
                    {"18d5202e-7871-48f0-85f5-fdebc25897ce","Uganda","UG","UGA","800",DateTimeOffset.Now}
                    ,
                    {"4949ddad-5b9c-4025-a4a6-1f15ebd58a3f","Ukraine","UA","UKR","804",DateTimeOffset.Now}
                    ,
                    {"8830916f-d073-4b92-b352-77d99533a780","United Arab Emirates","AE","ARE","784",DateTimeOffset.Now}
                    ,
                    {"59e43a39-6dcd-4477-8ce3-748008d3fcad","United Kingdom","GB","GBR","826",DateTimeOffset.Now}
                    ,
                    {"d34979c9-15d9-4d9f-8775-001e292aacc4","United States","US","USA","840",DateTimeOffset.Now}
                    ,
                    {"aac82345-e9cd-453c-997e-b5a5bb3c5fdd","United States Minor Outlying Islands","UM","UMI","581",DateTimeOffset.Now}
                    ,
                    {"244b48d9-df89-44d4-95b1-c2a931281770","United States Virgin Islands","VI","VIR","850",DateTimeOffset.Now}
                    ,
                    {"3fb86a9b-04ee-4b08-b833-1f7a53ba6705","Uruguay","UY","URY","858",DateTimeOffset.Now}
                    ,
                    {"05eb50b0-62a5-4b60-a236-8dd19c2b6108","Uzbekistan","UZ","UZB","860",DateTimeOffset.Now}
                    ,
                    {"5321234c-9063-48e4-b081-0335bf6881b5","Vanuatu","VU","VUT","548",DateTimeOffset.Now}
                    ,
                    {"633e1b2c-49bf-4f11-9a54-223448bbc256","Vatican City","VA","VAT","336",DateTimeOffset.Now}
                    ,
                    {"10e31f93-2656-4270-88d8-a7a69e7ad4fb","Venezuela","VE","VEN","862",DateTimeOffset.Now}
                    ,
                    {"7236389c-c18b-48ff-9b92-987ffdf1b657","Vietnam","VN","VNM","704",DateTimeOffset.Now}
                    ,
                    {"9194235d-edee-4596-b71b-ac8348be94e9","Wallis and Futuna","WF","WLF","876",DateTimeOffset.Now}
                    ,
                    {"95eacfa8-aa84-40ef-8af6-85b9c260c120","Western Sahara","EH","ESH","732",DateTimeOffset.Now}
                    ,
                    {"7ee13f71-b586-4645-a36a-72f760bab065","Yemen","YE","YEM","887",DateTimeOffset.Now}
                    ,
                    {"a5a4db1c-7e8e-4ef7-b3d5-448c8a5c2bc0","Zambia","ZM","ZMB","894",DateTimeOffset.Now}
                    ,
                    {"a6205437-3808-4c11-b4b0-54f6179b1746","Zimbabwe","ZW","ZWE","716",DateTimeOffset.Now}
                    ,
                    {"0EFB07E6-6634-46DE-A98D-A85BF331C20E","Worldwide","WW","WWE","000",DateTimeOffset.Now}

                },
                schema: "lookup");
            }
        }
    }
}
