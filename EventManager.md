# EventManager

## 介绍

实现对事件的统一管理

---

##结构

- EventManager：总管理器
  - EventType：事件类型，即事件码

---

##脚本

- EventType：enum枚举类型保存事件码
- EventManager：用于管理所有事件
  - Attributes：
    - allEvents：字典保存所有事件码以及对应函数指针
  - Methods：
    - RegisterEvent：注册事件
    - UnregisterEvent：注销事件
    - BroadcastEvent：广播事件
    - DelEventByType：通过事件码删除事件
    - DelEventAll：删除全部事件
    - 构造函数



- UsingAudioManager：一个使用AudioManager进行播放音乐的例子

