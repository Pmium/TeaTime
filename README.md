# TeaTime

## Step 5 - 重構 StoresController

- 調整 POST 的回傳內容
- 回傳 CreatedAtAction
  - 狀態碼 201 Created
  - 在 Response header 中夾帶 Location，指向這個新的資源
