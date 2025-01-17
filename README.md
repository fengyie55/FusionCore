# FusionCore
构建一款开源的上位机框架/PC控制框架
***
# FusionCore 模块命名及功能描述

## 1. FusionUI
* **功能**：提供用户界面的快速开发工具，包括窗口管理、控件库、主题配置等。
* **扩展子模块：**
   * **FusionUI.Designer**：界面设计工具，支持可视化布局。
   * **FusionUI.Theme**：主题和样式管理。

## 2. FusionCtrl
* **功能**：负责设备和系统的逻辑控制，包括命令调度、状态管理等。
* **扩展子模块：**
   * **FusionCtrl.Scheduler**：任务调度模块，支持实时任务分配。
   * **FusionCtrl.StateMachine**：状态机管理模块，用于复杂逻辑的实现。

## 3. FusionData
* **功能**：处理数据的采集、存储、转换和管理。
* **扩展子模块：**
   * **FusionData.Collector**：数据采集模块，支持多种数据源。
   * **FusionData.Analyzer**：数据分析模块，提供实时与离线分析功能。

## 4. FusionCom
* **功能**：提供设备通信接口，支持多种工业协议和自定义通信。
* **扩展子模块：**
   * **FusionCom.Modbus**：支持 Modbus 协议通信。
   * **FusionCom.OPCUA**：支持 OPC UA 协议通信。
   * **FusionCom.MQTT**：支持 MQTT 协议通信，适合 IoT 场景。

## 5. FusionIO
* **功能**：管理输入/输出设备接口，支持 GPIO、UART 等硬件控制。
* **扩展子模块：**
   * **FusionIO.Digital**：数字 I/O 模块。
   * **FusionIO.Analog**：模拟 I/O 模块。
   * **FusionIO.PWM**：PWM 信号生成模块。

## 6. FusionVision
* **功能**：提供图像处理与机器视觉支持，适用于检测和控制场景。
* **扩展子模块：**
   * **FusionVision.Detection**：目标检测模块。
   * **FusionVision.Recognition**：图像识别模块。
   * **FusionVision.Calibration**：视觉校准模块。

## 7. FusionSim
* **功能**：用于系统仿真与调试，支持虚拟设备和流程测试。
* **扩展子模块：**
   * **FusionSim.VirtualDevice**：虚拟设备模块。
   * **FusionSim.DebugTool**：调试工具模块。

## 8. FusionLog
* **功能**：统一的日志记录与分析工具，支持多种格式和存储目标。
* **扩展子模块：**
   * **FusionLog.Viewer**：日志查看工具。
   * **FusionLog.Analyzer**：日志分析模块。

## 9. FusionSecurity
* **功能**：提供系统安全性支持，包括用户认证、数据加密等。
* **扩展子模块：**
   * **FusionSecurity.Auth**：用户认证模块。
   * **FusionSecurity.Encrypt**：数据加密模块。

## 10. FusionConfig
* **功能**：系统配置管理模块，支持动态配置和多版本兼容。
* **扩展子模块：**
   * **FusionConfig.Editor**：配置编辑工具。
   * **FusionConfig.Loader**：配置加载模块。