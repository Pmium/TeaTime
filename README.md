# TeaTime

## Step 7 - 重構 OrdersController

- 建構式注入 ILogger
- POST 錯誤處理調整
  - 找不到符合條件的 Store 時，對 API 呼叫者回傳適當回應
  - 在 Log 中紀錄明確錯誤資訊
