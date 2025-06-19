import { http } from 'uview-plus'

// 获取授权信息
export const getAuthInfoApi = (params) => http.get('/api/alipay/getAuthInfo', params)

// 支付回调
export const notifyApi = (data) => http.post('/api/alipay/notify', data)

// 统一收单下单并支付页面接口
export const alipayTradePagePayApi = (data) => http.post('/api/alipay/alipayTradePagePay', data)

// 交易预创建
export const alipayPreCreateApi = (data) => http.post('/api/alipay/alipayPreCreate', data)

