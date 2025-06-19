import { http } from 'uview-plus'

// 生成JSAPI调起支付所需参数
export const generateParametersForJsapiPayApi = (data) => http.post('/api/sysWechatPay/generateParametersForJsapiPay', data)

// 微信支付统一下单获取Id(商户直连)
export const payTransactionApi = (data) => http.post('/api/sysWechatPay/payTransaction', data)

// 微信支付统一下单(商户直连)Native
export const payTransactionNativeApi = (data) => http.post('/api/sysWechatPay/payTransactionNative', data)

// 微信支付统一下单获取Id(服务商模式)
export const payPartnerTransactionApi = (data) => http.post('/api/sysWechatPay/payPartnerTransaction', data)

// 获取支付订单详情
export const payInfoApi = (params) => http.get('/api/sysWechatPay/payInfo', params)

// 微信支付成功回调(商户直连)
export const payCallBackApi = (data) => http.post('/api/sysWechatPay/payCallBack', data)

// 微信支付成功回调(服务商模式)
export const payPartnerCallBackApi = (data) => http.post('/api/sysWechatPay/payPartnerCallBack', data)

// 微信退款申请)
export const refundApi = (data) => http.post('/api/sysWechatPay/refund', data)

// 微信查询单笔退款)
export const refundByOutRefundNumberApi = (params) => http.get('/api/sysWechatPay/refundByOutRefundNumber', params)

// 微信支付订单号查询（校正）
export const payTransactionByIdApi = (params) => http.get('/api/sysWechatPay/payTransactionById', params)

// 微信商户订单号查询（校正）
export const payTransactionByOutTradeNumberApi = (params) => http.get('/api/sysWechatPay/payTransactionByOutTradeNumber', params)

// 获取支付记录分页列表
export const pageApi = (data) => http.post('/api/sysWechatPay/page', data)

// 根据支付Id获取退款信息列表
export const refundListApi = (params) => http.get('/api/sysWechatPay/refundList', params)

