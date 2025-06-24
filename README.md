[![Downloads](https://img.shields.io/github/downloads/LaFesta1749/MVP-Stats/total?label=Downloads&color=333333&style=for-the-badge)](https://github.com/LaFesta1749/MVP-Stats/releases/latest)
[![Discord](https://img.shields.io/badge/Discord-Join-5865F2?style=for-the-badge&logo=discord&logoColor=white)](https://discord.gg/PTmUuxuDXQ)

# MVP-Stats (Exiled Plugin)

## 🏆 Overview

**MVP-Stats** is a plugin that tracks and displays round-end MVPs (Most Valuable Players) based on configurable statistics like kills, escapes, and healing. Show your best players at the end of each round with a clean, automatic broadcast.

---

## 📊 Features

* Tracks player stats during the round (kills, escapes, revives, damage dealt, healing done, etc.)
* Announces the MVP(s) at the end of the round
* Configurable broadcast format and duration
* Supports multiple MVP categories
* Optional role-based exclusions (e.g. Spectator, SCPs)

---

## ⚙️ Configuration Example

```yaml
is_enabled: true
broadcast_duration: 8
categories:
  kills: true
  escapes: true
  revives: true
  damage: false
  healing: true
excluded_roles:
  - Spectator
  - Tutorial
message_format: "<color=yellow>{category}</color> MVP: <b>{name}</b> with <b>{value}</b>!"
```

---

## 🧠 How It Works

1. During the round, the plugin listens for events like `PlayerDied`, `PlayerEscaped`, `PlayerUsedMedicalItem`, etc.
2. At the end of the round (`RoundEnded`), it processes all tracked stats.
3. Displays the top player in each enabled category using `Map.Broadcast`.

---

## 📁 Installation

1. Download the latest `.dll` from [Releases](https://github.com/LaFesta1749/MVP-Stats/releases).
2. Place it into your server's `Exiled/Plugins/` directory.
3. Start the server once to generate the config file.
4. Edit the config in `Exiled/Configs/MVP-Stats.yml`.

---

## ✅ Supported MVP Categories

* **Kills** — based on `PlayerDied` events
* **Escapes** — tracked per player via `PlayerEscaping`
* **Revives** — counted on successful SCP-049 revives
* **Healing** — uses medkit usage and HP recovery events
* **Damage** — optional, if enabled (track total dealt damage)

---

## 🚫 Role Exclusions

Add any role you don’t want to be counted in MVP stats to the `excluded_roles` list. (E.g. Spectator, Tutorial, SCPs.)

---

## 👤 Author

Created by **LaFesta1749**

* GitHub: [LaFesta1749](https://github.com/LaFesta1749)
* Discord: [SCP Bulgaria](https://discord.gg/PTmUuxuDXQ)

---

## 📜 License

Licensed under the [MIT License](LICENSE).
