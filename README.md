# TeaTime

## Step 3 - 建立並註冊 InMemory 資料庫

- 安裝 Microsoft.EntityFrameworkCore.InMemory
    - 因為我們使用 .NET 6，所以套件版本記得要選擇 6.XX 版本
- 在 Models 資料夾中新增類別 TeaTimeContext 並繼承 DbContext
- Program.cs 註冊服務
