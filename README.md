# TeaTime# TeaTime

## Step 8 - 重構：隱藏資料細節

- 將資料庫相關處理移到 DataAccess 層
  - 將原本的 Models 改成 DbEntities 並重新命名
- 新增 Domain 層並加入 Store 與 Order 相關內容
- Controller 依修改結構後的內容調整
