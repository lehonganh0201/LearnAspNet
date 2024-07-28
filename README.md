# LearnAspNet
<h3>Câu lệnh tạo các trang CRUD trong .NET</h3>
dotnet aspnet-codegenerator razorpage -m LearnPageRazor.Models.Article -dc LearnPageRazor.Models.MyBlogContext -outDir Pages/Blog -udl --referenceScriptLibraries

<h3>Câu lệnh tạo các trang cho Identity</h3>
dotnet aspnet-codegenerator identity -dc LearnPageRazor.Models.MyBlogContext

<h3>Câu lệnh tạo 1 trang cshtml mới</h3>
dotnet new page -n Index -o Areas/Admin/Pages/Role