# Silent Gun (Global Game Jam 2025 Project)
# Gun & Bubble System

Bu proje, Unity kullanılarak Floppy Disk ekibiyle birlikte geliştirilmiş bir projedeir. Silah ve mermi (baloncuk) sistemini içermektedir. Oyuncu farklı mermi türleri arasında geçiş yapabilir ve her birinin farklı etkileri vardır.

## Özellikler

- **Silah Hareketi**: Silah, oyuncunun imleci yönlendirdiği yöne döner ve uygun açıda konumlanır.
- **Mermi Modları**: Ateş, su ve buz olmak üzere üç farklı atış modu bulunmaktadır.
- **Animasyon Yönetimi**: Silahın animasyonları tetikleyiciler aracılığıyla yönetilmektedir.
- **Mermi Etkileşimi**: Mermiler, çevredeki nesnelerle etkileşime girerek belirli efektleri tetikler.

## Kullanılan Kod Yapıları

### 1. **Enum Kullanımı**

Kodda farklı silah modlarını belirtmek için `enum` kullanılmıştır:

```csharp
public enum GunMode
{
    fireMode, IceMode, waterMode, Default
}
```

Bu sayede modlar arasında geçiş yapmak daha yönetilebilir hale gelmiştir.

### 2. **Input Kontrolleri**

Kullanıcı girişlerini işlemek için `Update()` metodunda `Input.GetKeyDown()` ve `Input.GetMouseButtonDown()` kullanılmıştır:

```csharp
if (Input.GetKeyDown(KeyCode.Alpha1)) { mode = GunMode.fireMode; }
```

Bu yöntem, kullanıcının hızlı bir şekilde mermi modlarını değiştirmesine olanak tanır.

### 3. **Silah Yönlendirme**

Silahın oyuncunun fare imlecine doğru döndürülmesi için `Mathf.Atan2()` fonksiyonu kullanılmıştır:

```csharp
float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
transform.rotation = Quaternion.Euler(0, 0, angle);
```

Bu sayede silah doğal bir şekilde yön değiştirir.

### 4. **Mermi Yönetimi ve Polimorfizm**

Farklı türde mermileri yönetmek için `Bubble` adlı bir temel sınıf oluşturulmuş ve farklı mermi türleri bu sınıftan türetilmiştir:

```csharp
public class FireBubble : Bubble { /* Ateş mermisi davranışı */ }
public class WaterBubble : Bubble { /* Su mermisi davranışı */ }
public class FreezeBubble : Bubble { /* Buz mermisi davranışı */ }
```

Bu sayede ortak fonksiyonlar `Bubble` sınıfında tanımlanıp, alt sınıflarda özelleştirilebilmektedir.

### 5. **Coroutine Kullanımı**

Animasyon ve efektlerin zamanlanması için `IEnumerator` kullanılarak coroutine metodları oluşturulmuştur:

```csharp
private IEnumerator ResetAnimationFlag()
{
    yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length - 0.1f);
    isAnimationPlaying = false;
}
```

Bu sayede belirli işlemler zamanla tetiklenebilmektedir.

## Kurulum ve Kullanım

1. Unity'de yeni bir proje oluşturun.
2. `Gun` ve `Bubble` sınıflarını içeren C# dosyalarını projenize ekleyin.
3. `Gun` sınıfını bir GameObject'e ekleyin ve gerekli referansları atayın.
4. Oyun çalıştırıldığında silah fareyi takip edecek ve farklı mermi türleri ile ateş edebilecektir.

## Bilinmesi Gerekenler

- **Animasyonlar**: Silahın farklı atış modlarına geçişi animasyonlar ile desteklenmiştir.
- **Fizik**: Mermilerin hareketi için `Rigidbody2D` kullanılmıştır.
- **Etkileşim Sistemi**: `IInteractable` arayüzü, mermilerin nesnelerle etkileşim kurmasını sağlamaktadır.
- **GameManager Entegrasyonu**: `BlackBoard` üzerinden oyun yöneticisi ile iletişim sağlanmaktadır.

Bu belge, projenin temel işleyişini anlamak ve katkıda bulunmak isteyen geliştiricilere rehberlik etmesi için hazırlanmıştır. Daha fazla bilgi için kod içerisinde yorumlara göz atabilirsiniz.
![image](https://github.com/user-attachments/assets/324434ab-9856-4732-a7ac-1ae7c9d0a233)

![image](https://github.com/user-attachments/assets/7ae018e3-0aa1-4ccd-9ba3-eb2245594919)

![image](https://github.com/user-attachments/assets/a0cdc57c-79d5-43c2-b869-c98069610c67)

![image](https://github.com/user-attachments/assets/f506301f-1ea3-445a-909d-1841d7ef8254)




