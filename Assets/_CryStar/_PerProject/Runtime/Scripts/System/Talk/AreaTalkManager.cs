using CryStar.Core;
using CryStar.Core.Enums;
using CryStar.Utility;
using Cysharp.Threading.Tasks;
using iCON.System;

namespace CryStar.PerProject
{
    /// <summary>
    /// エリアでの会話を管理するクラス
    /// </summary>
    public class AreaTalkManager : CustomBehaviour
    {
        private CharacterLocationManager _locationManager; // キャラクターの位置データを管理するクラス
        private InGameManager _inGameManager;
        
        #region Life cycle

        public override async UniTask OnAwake()
        {
            await base.OnAwake();
            ServiceLocator.Register(this, ServiceType.Local);
        }

        public override async UniTask OnBind()
        {
            await base.OnBind();
            _locationManager = ServiceLocator.GetLocal<CharacterLocationManager>();
            if (_locationManager == null)
            {
                LogUtility.Error($"[{typeof(AreaTalkManager)}] {typeof(CharacterLocationManager)} がローカルサービスから取得できませんでした");
            }
            
            _inGameManager = ServiceLocator.GetLocal<InGameManager>();
        }

        #endregion

        public void GetMessage()
        {
            _inGameManager.PlayStory(2);
        }
        
        /// <summary>
        /// Viewからタップされたキャラクターの位置情報が渡されるため
        /// それを元に適切なメッセージを返却する
        /// </summary>
        public (string name, string massage) GetMessage(LocationType location)
        {
            if (_locationManager == null)
            {
                // もう一度取得できるか試す
                _locationManager = ServiceLocator.GetLocal<CharacterLocationManager>();

                if (_locationManager == null)
                {
                    LogUtility.Error($"[{typeof(AreaTalkManager)}] {typeof(CharacterLocationManager)} がローカルサービスから取得できませんでした");
                    return (string.Empty, string.Empty);
                }
            }

            
            var character = _locationManager.GetCharacter(location);
            // TODO: マスタデータを参照してキャラクターの表示名を取得する
            var characterName = character switch
            {
                CharacterType.Khalil => "カリル",
                CharacterType.Filou => "フィルウ",
                CharacterType.Isha => "イーシャ",
                CharacterType.Yule => "ユール",
                _ => string.Empty
            };
            
            var text = character switch
            {
                CharacterType.Khalil => "どうかな、君が憧れた研究室は。びーっくりするくらい汚いでしょ。ふふふ、ごめんね、一秒でも研究したくて！",
                CharacterType.Filou => "疲れていませんか。早めに休むのも大切なことだから...なんですか僕をじっと見て。何か言いたげな表情に見えますが。",
                CharacterType.Isha => "あなた、学生にしてはしっかりしてるわね。分野、私と同じでしょう？これから、面倒見てあげてもいいくらい。",
                CharacterType.Yule => "君...勝手な行動はしないように。はぁ...一体、どうやってここへ立ち入ったんだか...言っておくけど、カリル博士の親戚という話は一切信じていないから。",
                _ => string.Empty
            };
            
            return (characterName, text);
        }
    }
}