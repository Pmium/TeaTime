# TeaTime

## Step 12 - 重構：使用 Repository 模式抽離對資料處理的依賴

- 新增 IStoresRepository 與 IOrdersRepository
- 新增 InMemoryStoresRepository 與 InMemoryOrdersRepository，繼承 Interface 並實現資料處理邏輯
- 修改 Services 改注入 Repository 使用服務
- 在 Program.cs 註冊服務對應
