
namespace UnturnedNPCMaker
{
    enum EConditionsParams
    {
        TYPE,
        LOGIC,
        ID,
        STATUS,
        RESET,
        VALUE,
        ALLOW_UNSET
    }
    enum EConditionTypes
    {
        NONE,
        EXPERIENCE,
        REPUTATION,
        FLAG_BOOL,
        FLAG_SHORT,
        QUEST,
        SKILLSET,
        ITEM,
        KILLS_ZOMBIE,
        KILLS_HORDE
    }
    enum ELogicTypes
    {
        NONE,
        LESS_THAN,
        LESS_THAN_OR_EQUAL_TO,
        EQUAL,
        NOT_EQUAL,
        GREATER_THAN_OR_EQUAL_TO,
        GREATER_THAN
    }
    enum EValueModificationTypes
    {
        NONE,
        ASSIGN,
        INCREMENT,
        DECREMENT
    }
    enum EQusetStatus
    {
        NONE,
        ACTIVE,
        READY,
        COMPLETED
    }
    enum ERewardTypes
    {
        NONE,
        EXPERIENCE,
        REPUTATION,
        FLAG_BOOL,
        FLAG_SHORT,
        FLAG_SHORT_RANDOM,
        QUEST,
        ITEM,
        ITEM_RANDOM,
        VEHICLE,
        TELEPORT,
        EVENT
    }
}
